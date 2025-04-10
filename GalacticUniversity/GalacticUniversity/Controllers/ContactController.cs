using Microsoft.AspNetCore.Mvc;
using GalacticUniversity.Core.FeedbackService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.FeedbackViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GalacticUniversity.Controllers
{
    public class ContactController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly UserManager<User> _userManager;

        public ContactController(IFeedbackService feedbackService, UserManager<User> userManager)
        {
            _feedbackService = feedbackService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FeedbackViewModel feedbackVM)
        {
            if (!ModelState.IsValid)
            {
                return View(feedbackVM);   
            }
                
                var feedback = new Feedback
                {
                    UserName = feedbackVM.UserName,
                    Email = feedbackVM.Email,
                    Text = feedbackVM.Text,
                    UserID = User.Identity.IsAuthenticated ? _userManager.GetUserId(User) : null
                };

                await _feedbackService.Add(feedback);

                // Return JSON success response for AJAX requests
                return Json(new { success = true, message = "Your message has been sent successfully!" });
            

            // Return JSON error response with validation errors
           
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            var feedbacks = _feedbackService.GetAll();
            return View(feedbacks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _feedbackService.Get(id);
            if (feedback == null)
            {
                return NotFound();
            }

            await _feedbackService.Delete(feedback);
            TempData["success"] = "Feedback read.";
            // Return success response
            return RedirectToAction("List");
        }
    }
}