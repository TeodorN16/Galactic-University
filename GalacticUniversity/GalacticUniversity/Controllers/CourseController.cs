using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        public CourseController(ICourseService courseService,ICategoryService categoryService)
        { 
            _categoryService= categoryService;
            _courseService = courseService; 
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var categories = _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "CategoryName");
            return View();
        }
        [HttpPost]
        public IActionResult Add(Course course)
        {
            
            _courseService.Add(course);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Category ct)
        { 
            
            return RedirectToAction("Index");
        }
    }
}
