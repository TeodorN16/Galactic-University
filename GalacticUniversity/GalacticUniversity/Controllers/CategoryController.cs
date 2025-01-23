using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
        public IActionResult Add(Category ct) 
        { 
            _categoryService.Add(ct);
            return RedirectToAction("Index");
        }
    }
}
