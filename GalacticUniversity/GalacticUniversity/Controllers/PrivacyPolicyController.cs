using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
