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
            if (!ModelState.IsValid)
            { 
                return View(cvm);
            }

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
            TempData["success"] = "Succesfully added course!";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {

            Course course = await _courseService.Get(id);
            if (course==null)
            {
                return NotFound();
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
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            var existingCourse = await _courseService.Get(cvm.ID);

            // Update the properties of the existing entity
            existingCourse.CourseName = cvm.CourseName;
            existingCourse.Description = cvm.Description;
            existingCourse.StartDate = cvm.StartDate;
            existingCourse.EndDate = cvm.EndDate;
            existingCourse.CategoryID = cvm.CategoryID;


            if (cvm.Image != null)
            {
                // Upload new image
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
                return NotFound();
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


            var userID = _userManager.GetUserId(User);


            await _userCourseService.JoinCourse(userID, courseId);
            TempData["success"] = "Check your profile!";
            return RedirectToAction("Index");
        }

    }
}