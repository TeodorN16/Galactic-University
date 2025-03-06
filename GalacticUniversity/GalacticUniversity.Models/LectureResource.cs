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
        public string FileUrl { get; set; }

        [NotMapped]
        public IFormFile File {get;set;}
        // oreign Keys
        [ForeignKey("Lecture")]
        public int LectureID { get; set; }
         
        // Navigation Properties
        public Lecture Lecture { get; set; }
    }
}
