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

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public Category Category { get; set; }
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Course> UserCourses { get; set; }
    }
}