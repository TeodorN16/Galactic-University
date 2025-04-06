using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("User")]

        public string UserID { get; set; }

        public User User { get; set; }
    }
}
