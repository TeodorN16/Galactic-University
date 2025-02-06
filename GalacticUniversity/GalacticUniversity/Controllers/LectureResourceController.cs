using GalacticUniversity.Core.LectureResourceService;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.LectureResource;
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
        public async Task<IActionResult> Index(LectureResourceViewModel lr)
        {
            var resources = _lectureResourceService.GetAll();
            var model =  resources.Include(l=>l.Lecture).Select(lr => new LectureResourceViewModel
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
        public async Task<IActionResult> Add()
        {

            var lectures =  _lectureService.GetAll();
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
        public async Task<IActionResult> Add(LectureResourceQueryViewModel lrvm)
        {

            var lectureResource = new LectureResource
            {
                
                ResourcePath = lrvm.ResourcePath,
                ResourceType = lrvm.ResourceType,
                LectureID = lrvm.LectureId,

            };
            await _lectureResourceService.Add(lectureResource);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
          
             LectureResource lectureResource = await _lectureResourceService.Get(id);

            
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
        public async Task<IActionResult> Edit(LectureResourceQueryViewModel lrvm)
        {
            var model = new LectureResource
            {
                ResourceID = lrvm.ID,
                ResourceType = lrvm.ResourceType,
                ResourcePath = lrvm.ResourcePath,
                LectureID = lrvm.LectureId,
               
            };
            await _lectureResourceService.Update(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _lectureResourceService.Delete(await _lectureResourceService.Get(id));
            return RedirectToAction("Index");
        }
    }
}
