using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GalacticUniversity.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ILectureService _lectureService;
        private readonly ICategoryService _categoryService;

        public CourseController(ICourseService courseService,ILectureService lectureService,ICategoryService categoryService)
        {
            _courseService = courseService;
            _lectureService = lectureService;
            _categoryService = categoryService; 
        }
        public IActionResult Index(CourseViewModel? filter)
        {
            var query = _courseService.GetAll().AsQueryable();
            if (filter.CategoryID!=null)
            {
                query = query.Where(c => c.CategoryID == filter.CategoryID);
            }
            if (filter.StartDate != null )
            {
                query = query.Where(c => c.StartDate >= filter.StartDate);
            }
            if (filter.EndDate!=null)
            {
                query = query.Where(c => c.EndDate <= filter.EndDate);
            }
            var categories = _categoryService.GetAll();
            var model = new CourseViewModel
            {
                CategoryID = filter.CategoryID,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Categories = new SelectList(categories, "CategoryID", "CategoryName"),
                Courses = query.Include(c => c.Category).ToList()
            };
            
            return View(model);
        }
        public IActionResult Add()
        {
            var lectures = _lectureService.GetAll();
            ViewBag.Lectures=new SelectList(lectures,"LectureID","LectureName");
            var categories = _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories,"CategoryID","CategoryName");
            return View();
        }
        [HttpPost]
        public IActionResult Add(Course course)
        {
            _courseService.Add(course);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id) 
        { 
            Course course = _courseService.Get(id);
            var categories = _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "CategoryName");
            return View(course);
        }
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _courseService.Update(course);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _courseService.Delete(_courseService.Get(id));
            return RedirectToAction("Index");
        }

    }
}
