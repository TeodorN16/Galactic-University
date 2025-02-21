using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

        //public string PictureURL { get; set; }
        public string? ImageURL { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        // Foreign Keys
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        // Navigation Properties
        public Category Category { get; set; }
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
        public ICollection<Comment> Comments { get; set; }

        public ICollection<Course> UserCourses { get; set; }
     
    }
}
