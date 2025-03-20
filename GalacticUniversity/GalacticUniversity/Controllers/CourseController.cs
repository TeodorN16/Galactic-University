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

        

        public CourseController(ICourseService courseService,ICategoryService categoryService,ILectureService lectureService, CloudinaryService cloudinaryService,IUserCourseService userCourseService,IUserService<User> userService, UserManager<User> userManager)
        { 
            _categoryService= categoryService;
            _courseService = courseService; 
            _lectureService = lectureService;
            _cloudinaryService = cloudinaryService;
            _userCourseService = userCourseService;
            _userService = userService;
            _userManager = userManager;
           
        }

        public async Task<IActionResult> Index(CourseViewModel? filter)
        {
            var currentUser = await _userManager.GetUserAsync(User); // Get the current logged-in user
            var userId = currentUser?.Id;

            var query = _courseService.GetAll().AsQueryable();

            // Apply filters
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

            // Get all courses the user has joined
            var joinedCourses = _userCourseService.GetAll()
                                                   .Where(u => u.UserID == userId)
                                                   .Select(u => u.CourseID)
                                                   .ToList();

            // Filter out the courses the user has already joined
            var availableCourses = query.Where(c => !joinedCourses.Contains(c.CourseID)).ToList();

            var categories = _categoryService.GetAll();

            var model = new CourseViewModel
            {
                CategoryID = filter.CategoryID,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Categories = new SelectList(categories, "CategoryID", "CategoryName"),
                Courses = availableCourses // Only courses the user hasn't joined yet
            };

            return View(model);
        }
        [Authorize(Roles ="Admin")]
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
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CourseQueryViewModel cvm,string ImagePath)
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
                ImageURL = uploadedImageURL ?? cvm.ImageURL // Ensure it saves the correct URL
            };

            await _courseService.Add(course);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id) 
        { 
            Course course = await _courseService.Get(id);
            var categories = _categoryService.GetAll();

            var model = new CourseQueryViewModel
            {
                ID = course.CourseID,
                CourseName = course.CourseName,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
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
            var course = new Course
            {
                CourseID = cvm.ID,
                CourseName = cvm.CourseName,
                Description = cvm.Description,
                StartDate = cvm.StartDate,
                EndDate = cvm.EndDate,
                CategoryID = cvm.CategoryID,
            };
            await _courseService.Update(course);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.Delete(await _courseService.Get(id));
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {   
            Course course = await _courseService.GetAll().Where(c=>c.CourseID==id)
                .Include(c=>c.Lectures)
                .ThenInclude(l=>l.LectureResources)
                .Include(c=>c.Category)
                .FirstOrDefaultAsync();

            return View(course);
        }
       
        public async Task<IActionResult> JoinCourse(int courseId)
        {

            TempData["success"] = "upeh. vijte si profile";
            var userID = _userManager.GetUserId(User);
            

            await _userCourseService.JoinCourse(userID, courseId);
            TempData["success"] = "upeh. vijte si profile";
            return RedirectToAction("Index");
        }

    }
}
