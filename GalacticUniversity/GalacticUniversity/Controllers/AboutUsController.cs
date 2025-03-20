using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
