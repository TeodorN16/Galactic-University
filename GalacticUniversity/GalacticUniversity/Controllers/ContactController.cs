using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
