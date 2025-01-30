using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
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
            var list = _categoryService.GetAll();
            return View(list);
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
        public IActionResult Edit(int id)
        {
            Category ct = _categoryService.Get(id);

            return View(ct);
        }
        [HttpPost]
        public IActionResult Edit(Category ct)
        {
          
            _categoryService.Update(ct);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(_categoryService.Get(id));
            return RedirectToAction("Index");
        }
    }
}
