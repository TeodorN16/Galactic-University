using System.Runtime.CompilerServices;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Core.LectureService;

using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using GalacticUniversity.Models.ViewModels.LectureViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Controllers
{
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;
        private readonly ICourseService _courseService;

        public LectureController(ILectureService lectureService,ICourseService courseService)
        {
           _lectureService = lectureService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            var courses = _courseService.GetAll();
            var model = _lectureService.GetAll().Select(l => new LectureViewModel
            {
                ID = l.LectureID,
                Name = l.LectureName,
                Description = l.Description,
                CourseID = l.CourseID ?? 0

            });
            return View(model);
        }
        public async Task<IActionResult> Add()
        {
            var courses = _courseService.GetAll();
            var model = new LectureViewModel()
            {
                Courses = courses.Select(c => new SelectListItem
                {
                    Value = c.CourseID.ToString(),
                    Text = c.CourseName,
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(LectureViewModel lvm)
        {
            var lecture = new Lecture
            {
                LectureName = lvm.Name,
                Description = lvm.Description,
                CourseID = lvm.CourseID,

            };
            await _lectureService.Add(lecture);
            TempData["success"] = "Succesfully added lecture";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Lecture lecture = await _lectureService.Get(id);
            var courses = _courseService.GetAll();

            var model = new LectureViewModel
            {
                ID = lecture.LectureID,
                Name = lecture.LectureName,
                Description = lecture.Description,
                Courses = courses.Select(c => new SelectListItem
                {
                    Value = c.CourseID.ToString(),
                    Text = c.CourseName,
                }).ToList()

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LectureViewModel lvm)
        {
            var lecture = new Lecture
            {
                LectureID=lvm.ID,
                LectureName = lvm.Name,
                Description = lvm.Description,
                CourseID = lvm.CourseID,
            };
            await _lectureService.Update(lecture);
            TempData["success"] = "Succesfully edited lecture";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
            await _lectureService.Delete(await _lectureService.Get(id));
            TempData["success"] = "Succesfully deleted lecture";
            return RedirectToAction("Index");
        }

    }
}
