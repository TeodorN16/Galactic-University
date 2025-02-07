using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels
{
    public class CourseQueryViewModel
    {
        public int ID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int CategoryID { get; set; }
        public List<SelectListItem> Categories { get; set; }

        

        
    }
}