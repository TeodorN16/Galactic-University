using System.Runtime.CompilerServices;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using GalacticUniversity.Models.ViewModels.LectureViewModels;
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
           
            var model = _lectureService.GetAll().Select(l=>new LectureViewModel 
            { 
                ID=l.LectureID,
                Name=l.LectureName,
                Description=l.Description
            }).ToList();
            return View(model);
        }
        public IActionResult Add()
        {
            var model = new LectureViewModel();
            return View();
        }
        [HttpPost]
        public IActionResult Add(LectureViewModel lvm)
        {
            var lecture = new Lecture
            {
                LectureName = lvm.Name,
                Description = lvm.Description,
            };
            _lectureService.Add(lecture);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Lecture lecture = _lectureService.Get(id);

            var model = new LectureViewModel
            {
                ID = lecture.LectureID,
                Name = lecture.LectureName,
                Description = lecture.Description,
            };
            return View(lecture);
        }

        [HttpPost]
        public IActionResult Edit(LectureViewModel lvm)
        {
            var lecture = new Lecture
            {
                LectureName = lvm.Name,
                Description = lvm.Description,
            };
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
