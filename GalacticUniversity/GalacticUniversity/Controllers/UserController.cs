using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
