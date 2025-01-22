using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.DataAccess.CourseRepository
{
    public class CourseRepository : Repository<Course>
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public ICollection<Course> GetCourseByCategory(string category)
        {
            return _context.courses
                           .Where(c => c.Category.CategoryName== category) 
                           .ToList(); 
        }


    }
}
