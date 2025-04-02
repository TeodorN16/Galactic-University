using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public int Rating { get; set; }

        // Foreign Keys
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
      
        public Course Course { get; set; }
    }
}
