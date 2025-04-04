using GalacticUniversity.Core.CommentService;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager,ICourseService courseService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _courseService = courseService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var comments = _commentService.GetAll();
            var model = comments.Select(c => new CommentViewModel
            {
                ID = c.CommentID,
                CommentText = c.CommentText,
                CommentDate = c.CommentDate,
                Rating = c.Rating,
                CourseID = c.CourseID
            });
            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var courses = _courseService.GetAll();

            CommentViewModel cvm = new CommentViewModel
            {
                Courses = courses.Select(c => new SelectListItem
                {
                    Value = c.CourseID.ToString(),
                    Text = c.CourseName
                }).ToList()

            };
            return View(cvm);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Add(CommentViewModel cvm)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (cvm.CourseID != 0 && string.IsNullOrEmpty(cvm.CommentText) == false)
            {
             

                var comment = new Comment
                {
                    CommentID = cvm.ID,
                    CommentText = cvm.CommentText,
                    CommentDate = DateTime.Now,
                    Rating = cvm.Rating > 0 ? cvm.Rating : 1, 
                    UserID = currentUser.Id,
                    CourseID = cvm.CourseID
                };

                await _commentService.Add(comment);
                TempData["success"] = "Comment added successfully!";

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Comment");
                }
                return RedirectToAction("Learn", "User", new { id = comment.CourseID });
            }
            if (!ModelState.IsValid || cvm.CourseID == 0 || string.IsNullOrEmpty(cvm.CommentText))
            {
                // Repopulate the courses dropdown
                var courses = _courseService.GetAll();
                cvm.Courses = courses.Select(c => new SelectListItem
                {
                    Value = c.CourseID.ToString(),
                    Text = c.CourseName
                }).ToList();

                TempData["error"] = "Please check your inputs and try again.";
                return View(cvm);
            }
            TempData["success"] = "Comment added!";
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var comment = await _commentService.Get(id);
            var courses = _commentService.GetAll(); 

            if (comment == null)
            {
                return NotFound();
            }

            var viewModel = new CommentViewModel
            {
                ID = comment.CommentID,
                CommentText = comment.CommentText,
                CommentDate = comment.CommentDate,
                Rating = comment.Rating,
                CourseID = comment.CourseID,
                Courses = courses.Select(c => new SelectListItem
                {
                    Value = c.CourseID.ToString(),
                    Text = c.Course.CourseName,
                }).ToList()

            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CommentViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            var existingComment = await _commentService.Get(cvm.ID);

            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.CommentText = cvm.CommentText;
            existingComment.Rating = cvm.Rating;

            await _commentService.Update(existingComment);

            return RedirectToAction("Details", "Course", new { id = existingComment.CourseID });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.Get(id);

            if (comment == null)
            {
                return NotFound();
            }

            int courseId = comment.CourseID;

            await _commentService.Delete(comment);
            if (User.IsInRole("Admin"))
            {
                TempData["success"] = "Succesfully deleted comment";
                return RedirectToAction("Index","Comment");
            }
            
            return RedirectToAction("Details", "Course", new { id = courseId });
        }
        public async Task<IActionResult> GetCommentsForCourse(int id)
        {
            var comments = _commentService.GetAll().Where(co => co.CourseID == id);
            return View(comments);
        }
    }
}