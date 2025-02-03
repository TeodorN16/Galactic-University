using System.Runtime.CompilerServices;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
           _lectureService = lectureService;
        }
        public IActionResult Index()
        {
           
            var list = _lectureService.GetAll();
            return View(list);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Lecture lecture)
        { 
            _lectureService.Add(lecture);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Lecture lecture = _lectureService.Get(id);
            return View(lecture);
        }

        [HttpPost]
        public IActionResult Edit(Lecture lecture)
        {
            _lectureService.Update(lecture);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id) 
        {
            _lectureService.Delete(_lectureService.Get(id));
            return RedirectToAction("Index");
        }

    }
}
