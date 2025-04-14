using GalacticUniversity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GalacticUniversity.DataAccess
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options): base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<LectureResource> lectureResources { get; set; }
        public DbSet<Lecture> lectures { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<UserCourses> userCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           

        }
    }
}
