using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GalacticUniversity.Models
{
    public class User:IdentityUser
    {
       
      
        public string UserName { get; set; }
        
        

        // Navigation Properties
        public ICollection<Comment> Comments { get; set; }


        public ICollection<UserCourses> UserCourses { get; set; }

        public ICollection<Certificate> Certificates { get; set; }


    }
}
