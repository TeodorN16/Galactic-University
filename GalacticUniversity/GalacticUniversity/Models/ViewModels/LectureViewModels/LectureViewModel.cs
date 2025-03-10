using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels.LectureViewModels
{
    public class LectureViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CourseID { get; set; }

        public List<SelectListItem> Courses { get; set; }

    }
}
