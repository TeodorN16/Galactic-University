using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
