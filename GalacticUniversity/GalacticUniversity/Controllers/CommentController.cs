using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
