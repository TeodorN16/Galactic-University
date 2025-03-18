using System.Security.Claims;
using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.UserService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.LectureResource;
using GalacticUniversity.Models.ViewModels.LectureViewModels;
using GalacticUniversity.Models.ViewModels;
using GalacticUniversity.Models.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GalacticUniversity.Models.ViewModels.CourseViewModels;
using GalacticUniversity.Utility;
using GalacticUniversity.Core.CourseService;

namespace GalacticUniversity.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserCourseService _userCourseService;
        private readonly IUserService<User> _userService;
        private readonly UserManager<User> _userManager;
        private readonly ICourseService _courseService;

        public UserController(IUserCourseService userCourseService,IUserService<User> userService, UserManager<User> userManager,ICourseService courseService)
        { 
            _userCourseService = userCourseService;
            _userService = userService;
            _userManager = userManager;
            _courseService = courseService;
        }
        public  IActionResult Index()
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
             .Include(uc => uc.Course)  // Include the related Course data
            .FirstOrDefault(uc => uc.UserID == user.Id && uc.CourseID == courseId);



            string certificateHtml = CertificateGenerator.GenerateCertificateHtml(
                user.UserName,
                userCourse.Course.CourseName,
                DateTime.Now
            );

            var certificate = new Certificate
            {
                UserID = user.Id,
                CourseID = courseId,
                Course = userCourse.Course,
                IssueDate = DateTime.Now,
                CertificateUrl = certificateHtml
            };

            var bytes = System.Text.Encoding.UTF8.GetBytes(certificateHtml);
            return File(bytes, "text/html", $"{userCourse.Course.CourseName}_Certificate_{user.UserName}.html");

            user.Certificates.Add(certificate);
        }







    }
}
