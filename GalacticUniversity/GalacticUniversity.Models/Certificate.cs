using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class Certificate
    {
        [Key]
        public int CertificateID { get; set; }

        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        



    }
}
