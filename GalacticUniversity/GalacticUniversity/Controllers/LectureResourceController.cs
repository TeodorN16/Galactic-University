using GalacticUniversity.Core.LectureResourceService;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class LectureResourceController : Controller
    {
        private readonly ILectureResourceService _lectureResourceService;

        public LectureResourceController(ILectureResourceService lectureResourceService)
        {
            _lectureResourceService = _lectureResourceService;
        }
        public IActionResult Index()
        {
            var list = _lectureResourceService.GetAll();
            return View(list);
        }
        public IActionResult Add()
        {
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
            LectureResource lectureResource = _lectureResourceService.Get(id);

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
