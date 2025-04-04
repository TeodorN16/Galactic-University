using GalacticUniversity.Core.CloudinaryService;
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
        private readonly CloudinaryService _cloudinaryService;
        public LectureResourceController(ILectureResourceService lectureResourceService, ILectureService lectureService,CloudinaryService cloudinaryService)
        {
            _lectureResourceService = lectureResourceService;
            _lectureService = lectureService;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<IActionResult> Index(LectureResourceViewModel lr)
        {
            var resources = _lectureResourceService.GetAll();
            var model =  resources.Include(l=>l.Lecture).Select(lr => new LectureResourceViewModel
            {
                ID=lr.ResourceID,
                FileUrl=lr.FileUrl,
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
            string uploadedImageURL = null;

            if (lrvm.File != null)
            {
                uploadedImageURL = await _cloudinaryService.UploadImageAsync(lrvm.File);
            }
            var lectureResource = new LectureResource
            {
                
                FileUrl = uploadedImageURL ?? lrvm.FileUrl,
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
                FileUrl = lectureResource.FileUrl,
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
            var uploadedImage = _cloudinaryService.UploadImageAsync(lrvm.File);
            var currentLecture = await _lectureResourceService.Get(lrvm.ID);

            var currentImage = currentLecture.FileUrl;

            
            var model = new LectureResource
            {
                ResourceID = lrvm.ID,
                FileUrl= uploadedImage.ToString() ?? lrvm.FileUrl,
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
