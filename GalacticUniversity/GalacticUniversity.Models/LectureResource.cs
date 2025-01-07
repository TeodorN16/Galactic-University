using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class LectureResource
    {
        [Key]
        public int ResourceID { get; set; }
        public string ResourceType { get; set; }
        public string ResourcePath { get; set; }

        // Foreign Keys
        [ForeignKey("Lecture")]
        public int LectureID { get; set; }

        // Navigation Properties
        public Lecture Lecture { get; set; }
    }
}
