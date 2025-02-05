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
            var model = new LectureResourceQueryViewModel
            {
                Lectures = lectures.Select(l => new SelectListItem
                {
                    Value = l.LectureID.ToString(),
                    Text = l.LectureName
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(LectureResourceQueryViewModel lrvm)
        {

            var lectureResource = new LectureResource
            {
                
                ResourcePath = lrvm.ResourcePath,
                ResourceType = lrvm.ResourceType,
                LectureID = lrvm.LectureId,

            };
            _lectureResourceService.Add(lectureResource);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            /* var lectures = _lectureService.GetAll();
             ViewBag.Lectures = new SelectList(lectures, "LectureID", "LectureName");*/
             LectureResource lectureResource = _lectureResourceService.Get(id);

            
            var model = new LectureResourceQueryViewModel
            {
                ID=lectureResource.ResourceID,
                ResourceType = lectureResource.ResourceType,
                ResourcePath = lectureResource.ResourcePath,
                LectureId = lectureResource.LectureID,
                Lectures = _lectureService.GetAll().Select(l => new SelectListItem
                {
                    Value = l.LectureID.ToString(),
                    Text = l.LectureName
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(LectureResourceQueryViewModel lrvm)
        {
            var model = new LectureResource
            {
                ResourceID = lrvm.ID,
                ResourceType = lrvm.ResourceType,
                ResourcePath = lrvm.ResourcePath,
                LectureID = lrvm.LectureId,
               
            };
            _lectureResourceService.Update(model);
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
