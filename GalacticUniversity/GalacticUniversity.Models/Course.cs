using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign Keys
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        // Navigation Properties
        public Category Category { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
