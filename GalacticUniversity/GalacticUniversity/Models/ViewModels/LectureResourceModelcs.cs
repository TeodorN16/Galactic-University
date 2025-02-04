using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels
{
    public class LectureResourceModelcs
    {
        public string? ResourceType { get; set; }

        public string? ResourcePath { get; set; }

        public int? LectureId { get; set; }
        public Lecture Lecture { get; set; }

    }
}
