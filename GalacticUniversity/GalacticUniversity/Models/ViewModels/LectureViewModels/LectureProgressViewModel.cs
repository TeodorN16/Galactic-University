using GalacticUniversity.Models.ViewModels.LectureResource;

namespace GalacticUniversity.Models.ViewModels.LectureViewModels
{
    public class LectureProgressViewModel
    {
        public int LectureID { get; set; }
        public string LectureName { get; set; }
        public bool IsUnlocked { get; set; }
        public List<LectureResourceViewModel> LectureResources { get; set; }
    }
}
