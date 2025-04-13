using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GalacticUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;

        public HomeController(ILogger<HomeController> logger,ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = _courseService.GetAll()
                .Include(c => c.Comments)
                .Include(c=>c.Lectures)
                .Include(c=>c.Category)
                .OrderByDescending(c => c.Comments.Count) 
                .Take(3) 
                .ToList();
            return View(courses);
        }

        public async Task<IActionResult> PrivacyPolicy()
        {
            return View();
        }
        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
        public async Task<IActionResult> TermsOfService()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
