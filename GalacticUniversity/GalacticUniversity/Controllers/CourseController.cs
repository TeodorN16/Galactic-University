using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Core.CloudinaryService;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.UserService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;


namespace GalacticUniversity.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly ILectureService _lectureService;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IUserCourseService _userCourseService;
        private readonly IUserService<User> _userService;
        private readonly UserManager<User> _userManager;



        public CourseController(ICourseService courseService, ICategoryService categoryService, ILectureService lectureService, CloudinaryService cloudinaryService, IUserCourseService userCourseService, IUserService<User> userService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _courseService = courseService;
            _lectureService = lectureService;
            _cloudinaryService = cloudinaryService;
            _userCourseService = userCourseService;
            _userService = userService;
            _userManager = userManager;

        }
        [Authorize(Roles = "User,Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(CourseViewModel? filter)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userId = currentUser?.Id;

            var query = _courseService.GetAll().AsQueryable();

            
            if (filter.CategoryID != null)
            {
                query = query.Where(c => c.CategoryID == filter.CategoryID);
            }
            if (filter.StartDate != null)
            {
                query = query.Where(c => c.StartDate >= filter.StartDate);
            }
            if (filter.EndDate != null)
            {
                query = query.Where(c => c.EndDate <= filter.EndDate);
            }

          
            var joinedCourses = _userCourseService.GetAll()
                                                   .Where(u => u.UserID == userId)
                                                   .Select(u => u.CourseID)
                                                   .ToList();

           
            var availableCourses = query.Where(c => !joinedCourses.Contains(c.CourseID)).ToList();

            var categories = _categoryService.GetAll();

            var model = new CourseViewModel
            {
                CategoryID = filter.CategoryID,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Categories = new SelectList(categories, "CategoryID", "CategoryName"),
                Courses = availableCourses 
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {

            var categories = _categoryService.GetAll();
            var lectures = _lectureService.GetAll();
            var model = new CourseQueryViewModel
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(5),
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                }).ToList(),
                Lectures = lectures.Select(l => new SelectListItem
                {
                    Value = l.LectureID.ToString(),
                    Text = l.LectureName,
                }).ToList(),

            };


            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CourseQueryViewModel cvm, string ImagePath)
        {
           

            string uploadedImageURL = null;

            if (cvm.Image != null)
            {
                uploadedImageURL = await _cloudinaryService.UploadImageAsync(cvm.Image);
            }

            var course = new Course
            {
                CourseName = cvm.CourseName,
                Description = cvm.Description,
                StartDate = cvm.StartDate,
                EndDate = cvm.EndDate,
                CategoryID = cvm.CategoryID,
                Lectures = _lectureService.GetAll()
                              .Where(l => l.LectureID == cvm.SelectedLecturesID)
                              .ToList(),
                ImageURL = uploadedImageURL ?? cvm.ImageURL 
            };

            await _courseService.Add(course);
            TempData["success"] = "Succesfully added course!";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {

            Course course = await _courseService.Get(id);
            if (course==null)
            {
                TempData["error"] = "Course not found";
                return RedirectToAction("Index");
            }
            var categories = _categoryService.GetAll();

            var model = new CourseQueryViewModel
            {
                ID = course.CourseID,
                CourseName = course.CourseName,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                ImageURL = course.ImageURL,
                CategoryID = course.CategoryID,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                }).ToList(),
            };


            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(CourseQueryViewModel cvm)
        { 

            var existingCourse = await _courseService.Get(cvm.ID);

           
            existingCourse.CourseName = cvm.CourseName;
            existingCourse.Description = cvm.Description;
            existingCourse.StartDate = cvm.StartDate;
            existingCourse.EndDate = cvm.EndDate;
            existingCourse.CategoryID = cvm.CategoryID;


            if (cvm.Image != null)
            {
                
                string uploadedImageURL = await _cloudinaryService.UploadImageAsync(cvm.Image);
                existingCourse.ImageURL = uploadedImageURL;
            }
            else if (!string.IsNullOrEmpty(cvm.ImageURL))
            {

                existingCourse.ImageURL = cvm.ImageURL;
            }



            await _courseService.Update(existingCourse);

            TempData["success"] = "Successfully edited course!";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var course = await _courseService.Get(id);
            if (course==null)
            {
                TempData["error"] = "Course not found";
                return RedirectToAction("Index");
            }

            await _courseService.Delete(course);
            TempData["success"] = "Succesfully deleted course!";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
           

            Course course = await _courseService.GetAll().Where(c => c.CourseID == id)
                .Include(c => c.Lectures)
                .ThenInclude(l => l.LectureResources)
                .Include(c => c.Category)
                .Include(c => c.Comments).ThenInclude(comment => comment.User)
                .FirstOrDefaultAsync();

            return View(course);
        }

        public async Task<IActionResult> JoinCourse(int courseId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userCourses = _userCourseService.GetAll();

            
            if (userCourses.Any(uc => uc.CourseID == courseId && uc.UserID == currentUser.Id))
            {
                
                TempData["error"] = "You have already enrolled to this course.";
                return RedirectToAction("Index", "Course");  
            }


            var userID = _userManager.GetUserId(User);


            await _userCourseService.JoinCourse(userID, courseId);
            TempData["success"] = "Check your profile!";
            return RedirectToAction("Index");
        }

    }
}