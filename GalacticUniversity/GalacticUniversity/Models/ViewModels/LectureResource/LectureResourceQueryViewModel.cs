using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels.LectureResource
{
    public class LectureResourceQueryViewModel
    {
        public int ID { get; set; }
        public string ResourceType { get; set; }

        public string ResourcePath { get; set; }

        public int LectureId { get; set; }
        public List<SelectListItem> Lectures { get; set; }



    }
}