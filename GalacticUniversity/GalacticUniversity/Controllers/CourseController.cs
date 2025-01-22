using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
