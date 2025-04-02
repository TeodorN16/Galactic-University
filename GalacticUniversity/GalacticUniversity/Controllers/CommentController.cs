using GalacticUniversity.Core.CommentService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        { 
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            var comments =_commentService.GetAll();
            var model = comments.Select(c => new CommentViewModel
            {
                ID = c.CommentID,
                CommentText = c.CommentText,
                CommentDate = c.CommentDate,
                Rating = c.Rating,
            });
            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var model = new CommentViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentViewModel cvm)
        {
            var comment = new Comment
            {
                CommentID = cvm.ID,
                CommentText = cvm.CommentText,
                CommentDate = cvm.CommentDate,
                Rating = cvm.Rating,
            };
            await _commentService.Add(comment);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var comment = _commentService.Get(id);
            return View(comment);
        }
        
    }
}
