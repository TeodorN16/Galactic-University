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
        public async Task<IActionResult> Index()
        {
            var categories = _categoryService.GetAll();
            var models= categories.Select(c=>new CategoryQueryViewModel 
            { 
                ID=c.CategoryID,
                Name=c.CategoryName
            }).ToList();

            return View(models);
        }
        public async Task<IActionResult> Add()
        {
            var model = new CategoryViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel ctvm)
        {
            var category = new Category
            {
                CategoryName = ctvm.Name,

            };
            await _categoryService.Add(category);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        { 
            Category ct = await _categoryService.Get(id);
            var model = new CategoryViewModel
            {
                ID = ct.CategoryID,
                Name = ct.CategoryName
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel ctvm)
        {

            var category = new Category
            {
                CategoryID = ctvm.ID,
                CategoryName = ctvm.Name
            };
            await _categoryService.Update(category);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(await _categoryService.Get(id));
            return RedirectToAction("Index");
        }
    }
}
