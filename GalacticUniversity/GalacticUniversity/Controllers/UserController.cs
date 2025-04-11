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
using SelectPdf;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;

namespace GalacticUniversity.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserCourseService _userCourseService;
        private readonly IUserService<User> _userService;
        private readonly UserManager<User> _userManager;
        private readonly ICourseService _courseService;
        private readonly CloudinaryService _cloudinaryService;

        public UserController(IUserCourseService userCourseService, IUserService<User> userService, 
               UserManager<User> userManager, ICourseService courseService, CloudinaryService cloudinaryService)
        {
            _userCourseService = userCourseService;
            _userService = userService;
            _userManager = userManager;
            _courseService = courseService;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult Index()
        {
            var users =  _userService.GetAll().Include(u => u.Comments)
        .Include(u => u.UserCourses)
        .Include(u => u.Certificates)
        .ToList(); ;
            return View(users);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["error"] = "User not found";
                return RedirectToAction("Index");
            }

            var user = await _userManager.Users
                .Include(u => u.Comments)
                .Include(u => u.UserCourses)
                    .ThenInclude(uc => uc.Course)
                .Include(u => u.Certificates)
                    .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);

            var viewModel = new UserDetailsViewModel
            {
                User = user,
                Roles = roles.ToList()
            };

            return View(viewModel);
        }
       
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["error"] = "User not found";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var model = new UserEditViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            TempData["success"] = "Profile updated successfully";
            return RedirectToAction("MyProfile");



        }

        

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["error"] = "User not found";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            await _userManager.DeleteAsync(user);
            TempData["success"] = "Succesfully deleted the user";
            return RedirectToAction("Index");
        }

    

        public async Task<IActionResult> MyProfile(UserViewModel? uvm)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                TempData["error"] = "User not found";
                return RedirectToAction("Index");
            }

            var courses = _userCourseService
                .GetAll()
                .Where(u => u.UserID == currentUser.Id)
                .Include(u => u.Course) 
                    .ThenInclude(c => c.Lectures) 
                    .ThenInclude(l => l.LectureResources)
                .Select(u => u.Course) 
                .ToList();

            var model = new UserViewModel
            {
                Id = currentUser.Id,
                Email = currentUser.Email,
                UserName = currentUser.UserName,
                Courses = courses
            };

            return View(model);
        }
        public async Task<IActionResult> Learn(int id)
        {
          
            if (id == 0)
            {
                return BadRequest("Invalid course ID.");
            }

           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

           
            var userCourses = _userCourseService
                .GetAll()
                .Where(u => u.UserID == user.Id)
                .Include(u => u.Course) 
                    .ThenInclude(c => c.Lectures) 
                    .ThenInclude(l => l.LectureResources) 
                .Select(u => u.Course)
                .ToList();

            
            var course = userCourses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
            {
                return NotFound("Course not found.");
            }

           
            var userCourse = _userCourseService.GetAll()
                .FirstOrDefault(u => u.UserID == user.Id && u.CourseID == id);

            
            int? lastLectureId = userCourse?.LectureID;

           
            var firstLecture = course.Lectures.FirstOrDefault();
            if (lastLectureId == null && firstLecture != null)
            {
                lastLectureId = firstLecture.LectureID;
            }

           
            var model = new CourseProgressViewModel
            {
                Course = course,
                LastLectureID = lastLectureId
            };

           
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProgress(int courseId, int lectureId)
        {
          
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

       
            var userCourse = _userCourseService.GetAll()
                .FirstOrDefault(u => u.UserID == user.Id && u.CourseID == courseId);

            if (userCourse == null)
            {
                return NotFound("User course not found");
            }

            
            userCourse.LectureID = lectureId;

            await _userCourseService.Update(userCourse);


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

           
            var existingCertificate = await _userManager.Users
                .Include(u => u.Certificates)
                .Where(u => u.Id == user.Id)
                .SelectMany(u => u.Certificates)
                .FirstOrDefaultAsync(c => c.CourseID == courseId);

            if (existingCertificate != null)
            {
               
                TempData["Error"] = "You have already received a certificate for this course.";
                return RedirectToAction("Learn", new { id = courseId });
            }

            
            string certificateHtml = CertificateGenerator.GenerateCertificateHtml(
                user.UserName,
                userCourse.Course.CourseName,
                DateTime.Now
            );

           
            certificateHtml = @"<!DOCTYPE html>
                            <html>
                            <head>
                                    <title>Certificate for " + userCourse.Course.CourseName + @"</title>
                                    <meta charset=""utf-8"">
                                </head>
                            <body>
                            " + certificateHtml + @"
                            </body>
                            </html>";

            
            string fileName = $"{userCourse.Course.CourseName}Certificate{user.UserName}.html";
            string tempPath = Path.GetTempFileName();
            await System.IO.File.WriteAllTextAsync(tempPath, certificateHtml);

            string cloudinaryUrl;

            using (var fileStream = new FileStream(tempPath, FileMode.Open))
            {
              
                var formFile = new FormFile(fileStream, 0, fileStream.Length, "certificate", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/html"
                };

                var uploadResult = await _cloudinaryService.UploadImageAsync(formFile);
               
            }

            System.IO.File.Delete(tempPath);

           
            var certificate = new Certificate
            {
                UserID = user.Id,
                CourseID = courseId,
                Course = userCourse.Course,
                IssueDate = DateTime.Now,
                CertificateUrl = "URL not available"
            };

            if (user.Certificates == null)
                user.Certificates = new List<Certificate>();

            user.Certificates.Add(certificate);
            await _userManager.UpdateAsync(user);

         
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(certificateHtml);
            return File(bytes, "text/html", fileName);
        }
    }




}
