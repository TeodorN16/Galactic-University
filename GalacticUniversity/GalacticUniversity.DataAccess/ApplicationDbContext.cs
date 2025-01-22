using GalacticUniversity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GalacticUniversity.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<LectureResource> lectureResources { get; set; }
        public DbSet<Lecture> lectures { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Category> categories { get; set; }

    }
}
