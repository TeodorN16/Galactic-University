using GalacticUniversity.Core.TarotCardService;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class TarotController : Controller
    {
        private readonly TarotService _tarotService;

        public TarotController(TarotService tarotService)
        {
            _tarotService = tarotService;
        }

        public IActionResult Index()
        {
            var viewModel = new TarotViewModel
            {
                HasReading = false
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetReading(string readingType)
        {
            if (string.IsNullOrEmpty(readingType) ||
                (readingType != "Love" && readingType != "Career" && readingType != "General"))
            {
                return BadRequest("Invalid reading type");
            }

            var reading = _tarotService.GetReading(readingType);

            return Json(reading);
        }
    }
}
