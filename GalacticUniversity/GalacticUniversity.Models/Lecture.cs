using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class Lecture
    {
        [Key]
        public int LectureID { get; set; }
        public string LectureName { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        // Navigation Properties
        public Course Course { get; set; }
        public ICollection<LectureResource> LectureResources { get; set; }
    }
}
