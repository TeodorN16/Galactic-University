using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels
{
    public class LectureResourceViewModel
    {
        public int ID { get; set; }
        public string ResourceType { get; set; }

        public string ResourcePath { get; set; }

        public int LectureId { get; set; }
        public string LectureName { get; set; }

       

    }
}
