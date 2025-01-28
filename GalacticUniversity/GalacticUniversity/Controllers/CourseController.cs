using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        { 
            _courseService = courseService; 
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Course course)
        {
            _courseService.Add(course);
            return RedirectToAction("Index");
        }
<<<<<<< HEAD

=======
        [HttpPost]
        public IActionResult Delete(Category ct)
        { 
            
            return RedirectToAction("Index");
        }
>>>>>>> origin/main
    }
}
