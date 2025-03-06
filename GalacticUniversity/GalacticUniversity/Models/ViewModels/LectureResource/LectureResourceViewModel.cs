using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels.LectureResource
{
    public class LectureResourceViewModel
    {
        public int ID { get; set; }
        public string FileUrl { get; set; }
        public IFormFile File { get; set; }
        public int LectureId { get; set; }
        public string LectureName { get; set; }



    }
}
