using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels
{
    public class CourseViewModel
    {
        public int? CategoryID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public SelectList Categories { get; set; }

        public List<Course> Courses { get; set; }
    }
}
