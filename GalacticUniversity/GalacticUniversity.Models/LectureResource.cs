using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GalacticUniversity.Models
{
    public class LectureResource
    {
        [Key]
        public int ResourceID { get; set; }
        [Required]
        public string FileUrl { get; set; }

        [NotMapped]
        public IFormFile File {get;set;}
       
        [ForeignKey("Lecture")]
        public int LectureID { get; set; }
         
 
        public Lecture Lecture { get; set; }
    }
}
