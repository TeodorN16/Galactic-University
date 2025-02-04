using GalacticUniversity.Core.LectureResourceService;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GalacticUniversity.Controllers
{
    public class LectureResourceController : Controller
    {
        private readonly ILectureResourceService _lectureResourceService;
        private readonly ILectureService _lectureService;
        public LectureResourceController(ILectureResourceService lectureResourceService, ILectureService lectureService)
        {
            _lectureResourceService = lectureResourceService;
            _lectureService = lectureService;
        }
        public IActionResult Index(LectureResourceViewModel lr)
        {
            var model = _lectureResourceService.GetAll().Include(l=>l.Lecture).Select(lr => new LectureResourceViewModel
            {
                ID=lr.ResourceID,
                ResourcePath = lr.ResourcePath,
                ResourceType = lr.ResourceType,
                LectureId = lr.LectureID,
                LectureName=lr.Lecture.LectureName
            }).ToList();
            //var list = _lectureResourceService.GetAll().Include(lr=>lr.Lecture);
            return View(model);
        }
        public IActionResult Add()
        {

            var lectures = _lectureService.GetAll();
            ViewBag.Lectures = new SelectList(lectures, "LectureID", "LectureName");
            return View();
        }
        [HttpPost]
        public IActionResult Add(LectureResource lectureResource)
        {

            _lectureResourceService.Add(lectureResource);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var lectures = _lectureService.GetAll();
            ViewBag.Lectures = new SelectList(lectures, "LectureID", "LectureName");
            LectureResource lectureResource = _lectureResourceService.Get(id);
           
            ViewBag.Lectures = new SelectList(lectures, "LectureID", "LectureName");
            return View(lectureResource);
        }
        [HttpPost]
        public IActionResult Edit(LectureResource lectureResource)
        {

            _lectureResourceService.Update(lectureResource);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _lectureResourceService.Delete(_lectureResourceService.Get(id));
            return RedirectToAction("Index");
        }
    }
}
