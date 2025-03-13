using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class UserCourses
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        public User User { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Lecture")]

        public int? LectureID { get; set; }

        public Lecture Lecture { get; set; }



    }
}
