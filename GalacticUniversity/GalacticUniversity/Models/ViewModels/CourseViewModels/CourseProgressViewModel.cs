using GalacticUniversity.Models.ViewModels.LectureViewModels;

namespace GalacticUniversity.Models.ViewModels.CourseViewModels
{
    public class CourseProgressViewModel
    {
        public Course Course { get; set; }
        public int? LastLectureID { get; set; }
    }
}
