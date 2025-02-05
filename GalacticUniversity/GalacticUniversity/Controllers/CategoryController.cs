using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using GalacticUniversity.Models.ViewModels.CategoryViewModels;
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
            var models=_categoryService.GetAll().Select(c=>new CategoryQueryViewModel 
            { 
                ID=c.CategoryID,
                Name=c.CategoryName
            }).ToList();

            return View(models);
        }
        public IActionResult Add()
        {
            var model = new CategoryViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(CategoryViewModel ctvm)
        {
            var category = new Category
            {
                CategoryName = ctvm.Name,

            };
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        { 
            Category ct = _categoryService.Get(id);
            var model = new CategoryViewModel
            {
                ID = ct.CategoryID,
                Name = ct.CategoryName
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel ctvm)
        {

            var category = new Category
            {
                CategoryID = ctvm.ID,
                CategoryName = ctvm.Name
            };
            _categoryService.Update(category);
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
