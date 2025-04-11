using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using GalacticUniversity.Models.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = _categoryService.GetAll();
            var models = categories.Select(c => new CategoryQueryViewModel
            {
                ID = c.CategoryID,
                Name = c.CategoryName
            }).ToList();

            return View(models);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var model = new CategoryViewModel();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CategoryViewModel ctvm)
        {
          
            var category = new Category
            {
                CategoryName = ctvm.Name,

            };
            await _categoryService.Add(category);
            TempData["success"] = "Successfully added a category";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Category ct = await _categoryService.Get(id);
            if (ct==null)
            {
                TempData["error"] = "Category not found";
                return RedirectToAction("Index");
            }
            var model = new CategoryViewModel
            {
                ID = ct.CategoryID,
                Name = ct.CategoryName
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CategoryViewModel ctvm)
        {
        

            var category = new Category
            {
                CategoryID = ctvm.ID,
                CategoryName = ctvm.Name
            };
            await _categoryService.Update(category);
            TempData["success"] = "Successfully edited category";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.Get(id);
            if (category==null)
            {
                TempData["error"] = "Category not found";
                return RedirectToAction("Index");
            }
            await _categoryService.Delete(category);
            TempData["success"] = "Successfully deleted category";
            return RedirectToAction("Index");
        }
    }
}
