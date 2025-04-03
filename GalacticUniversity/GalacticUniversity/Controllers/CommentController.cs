using GalacticUniversity.Core.CommentService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
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

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Add(CommentViewModel cvm)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Course", new { id = cvm.CourseID });
            }

            var comment = new Comment
            {
                CommentID = cvm.ID,
                CommentText = cvm.CommentText,
                CommentDate = DateTime.Now,
                Rating = cvm.Rating,
                UserID = currentUser.Id,
                CourseID = cvm.CourseID
            };

            await _commentService.Add(comment);

            // Redirect back to the course details page
            return RedirectToAction("Learn", "User", new { id = comment.CourseID });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var comment = await _commentService.Get(id);

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
                CourseID = comment.CourseID
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
            TempData["success"] = "Succesfully deleted comment";
            return RedirectToAction("Details", "Course", new { id = courseId });
        }
        public async Task<IActionResult> GetCommentsForCourse(int id)
        {
            var comments = _commentService.GetAll().Where(co => co.CourseID == id);
            return View(comments);
        }
    }
}