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
        public async Task<IActionResult> Index()
        {
           
            var model = _lectureService.GetAll().Select(l=>new LectureViewModel 
            { 
                ID=l.LectureID,
                Name=l.LectureName,
                Description=l.Description
            }).ToList();
            return View(model);
        }
        public async Task<IActionResult> Add()
        {
            var model = new LectureViewModel();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(LectureViewModel lvm)
        {
            var lecture = new Lecture
            {
                LectureName = lvm.Name,
                Description = lvm.Description,
            };
            await _lectureService.Add(lecture);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Lecture lecture = await _lectureService.Get(id);

            var model = new LectureViewModel
            {
                ID = lecture.LectureID,
                Name = lecture.LectureName,
                Description = lecture.Description,
            };
            return View(lecture);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LectureViewModel lvm)
        {
            var lecture = new Lecture
            {
                LectureName = lvm.Name,
                Description = lvm.Description,
            };
            await _lectureService.Update(lecture);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
            await _lectureService.Delete(await _lectureService.Get(id));
            return RedirectToAction("Index");
        }

    }
}
