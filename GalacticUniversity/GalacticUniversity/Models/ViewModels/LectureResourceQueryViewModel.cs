using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels
{
    public class LectureResourceQueryViewModel
    {
        
        public string ResourceType { get; set; }

        public string ResourcePath { get; set; }

        public int LectureId { get; set; }
        public List<SelectListItem> Lectures { get; set; }



    }
}