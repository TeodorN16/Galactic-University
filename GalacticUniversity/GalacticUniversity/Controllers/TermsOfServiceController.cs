using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class TermsOfServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
