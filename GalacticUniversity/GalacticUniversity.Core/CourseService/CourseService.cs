using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> repo;
        public CourseService(IRepository<Course>_repo)
        {
            repo = _repo;
        }
        public void Add(Course obj)
        {
            repo.Add(obj);
        }

        public void Delete(Course obj)
        {
            repo.Delete(obj);
        }

        public Course Get(int id)
        {
            Course course = repo.Get(id);
            return course;
        }

        public IQueryable<Course> GetAll()
        {
            return repo.GetAll();
        }

        public List<Course> GetCourseByCategory(string category)
        {
            Expression<Func<Course, bool>> filter = course => course.Category.CategoryName == category;
            return repo.Find(filter);
        }

        public void Update(Course obj)
        {
            repo.Update(obj);
        }
    }
}
