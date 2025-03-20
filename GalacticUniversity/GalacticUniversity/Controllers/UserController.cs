using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.UserService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GalacticUniversity.Models.ViewModels.CourseViewModels;
using GalacticUniversity.Utility;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Core.CloudinaryService;
using System.IO;
using System.Net.Mime;
namespace GalacticUniversity.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserCourseService _userCourseService;
        private readonly IUserService<User> _userService;
        private readonly UserManager<User> _userManager;
        private readonly ICourseService _courseService;
        private readonly CloudinaryService _cloudinaryService;

        public UserController(IUserCourseService userCourseService, IUserService<User> userService, UserManager<User> userManager, ICourseService courseService, CloudinaryService cloudinaryService)
        {
            _userCourseService = userCourseService;
            _userService = userService;
            _userManager = userManager;
            _courseService = courseService;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MyProfile(UserViewModel? uvm)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            var courses = _userCourseService
                .GetAll()
                .Where(u => u.UserID == currentUser.Id)
                .Include(u => u.Course) // Include the Course entity
                    .ThenInclude(c => c.Lectures) // Include Lectures for each Course
                    .ThenInclude(l => l.LectureResources) // Include LectureResources for each Lecture
                .Select(u => u.Course) // Select the Course data
                .ToList();

            var model = new UserViewModel
            {
                Id = currentUser.Id,
                Email = currentUser.Email,
                Courses = courses
            };

            return View(model);
        }
        public async Task<IActionResult> Learn(int id)
        {
            // Check if ID is provided
            if (id == 0)
            {
                return BadRequest("Invalid course ID.");
            }

            // Get the current user from UserManager
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Get the user's enrolled courses and include necessary data
            var userCourses = _userCourseService
                .GetAll()
                .Where(u => u.UserID == user.Id)
                .Include(u => u.Course) // Include the Course entity
                    .ThenInclude(c => c.Lectures) // Include Lectures for each Course
                    .ThenInclude(l => l.LectureResources) // Include LectureResources for each Lecture
                .Select(u => u.Course) // Select the Course data
                .ToList();

            // Find the specific course for the user
            var course = userCourses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
            {
                return NotFound("Course not found.");
            }

            // Retrieve the user's last selected lecture (if any)
            var userCourse = _userCourseService.GetAll()
                .FirstOrDefault(u => u.UserID == user.Id && u.CourseID == id);

            // If a UserCourse record exists, fetch the last selected lecture ID
            int? lastLectureId = userCourse?.LectureID;

            // If the lastLectureId is null, default to the first lecture
            var firstLecture = course.Lectures.FirstOrDefault();
            if (lastLectureId == null && firstLecture != null)
            {
                lastLectureId = firstLecture.LectureID;
            }

            // Pass the course and the last selected lecture ID to the view
            var model = new CourseProgressViewModel
            {
                Course = course,
                LastLectureID = lastLectureId
            };

            // Return the course progress view with the data
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProgress(int courseId, int lectureId)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Get the user's course record
            var userCourse = _userCourseService.GetAll()
                .FirstOrDefault(u => u.UserID == user.Id && u.CourseID == courseId);

            if (userCourse == null)
            {
                return NotFound("User course not found");
            }

            // Update the LastLectureID
            userCourse.LectureID = lectureId;

            await _userCourseService.Update(userCourse);

            // Redirect back to the Learn page
            return RedirectToAction("Learn", new { id = courseId });

        }
        [HttpPost]
        public async Task<IActionResult> GenerateCertificate(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var userCourse = _userCourseService.GetAll()
                .Include(uc => uc.Course)
                .FirstOrDefault(uc => uc.UserID == user.Id && uc.CourseID == courseId);

            if (userCourse == null)
                return NotFound("Course not found for this user");

            string certificateHtml = CertificateGenerator.GenerateCertificateHtml(
                user.UserName,
                userCourse.Course.CourseName,
                DateTime.Now
            );

            // Convert HTML to PDF
            var converter = new HtmlToPdf();
            var pdfDoc = converter.ConvertHtmlString(certificateHtml);
            byte[] bytes = pdfDoc.Save();

            // Create filename
            string fileName = $"{userCourse.Course.CourseName}Certificate{user.UserName}.pdf";

            // Create temporary file for Cloudinary upload
            string tempPath = Path.GetTempFileName();
            await System.IO.File.WriteAllBytesAsync(tempPath, bytes);

            // Upload to Cloudinary
            string cloudinaryUrl;
            using (var fileStream = new FileStream(tempPath, FileMode.Open))
            {
                var file = new FormFile(fileStream, 0, fileStream.Length, "certificate", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/pdf",
                };

                // Add your Cloudinary upload logic here
                // For example, if using CloudinaryDotNet:
                // var uploadParams = new FileUploadParams()
                // {
                //     File = new FileDescription(fileName, fileStream)
                // };
                // var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                // cloudinaryUrl = uploadResult.SecureUri.ToString();
                cloudinaryUrl = "your-cloudinary-upload-logic-here";
            }

            // Delete the temporary file
            System.IO.File.Delete(tempPath);

            // Create and save certificate record
            var certificate = new Certificate
            {
                UserID = user.Id,
                CourseID = courseId,
                Course = userCourse.Course,
                IssueDate = DateTime.Now,
                CertificateUrl = cloudinaryUrl ?? "URL not available"
            };

            if (user.Certificates == null)
                user.Certificates = new List<Certificate>();

            user.Certificates.Add(certificate);
            await _userManager.UpdateAsync(user);

            // Return the file
            return File(bytes, "application/pdf", fileName);
        }
    }
    
}
