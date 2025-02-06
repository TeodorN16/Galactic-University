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
        public async Task Add(Course obj)
        {
            await repo.Add(obj);
        }

        public async Task Delete(Course obj)
        {
            await repo.Delete(obj);
        }

        public async Task<Course> Get(int id)
        {
            Course course = await repo.Get(id);
            return course;
        }

        public  IQueryable<Course> GetAll()
        {
            return  repo.GetAll();
        }

        public async Task<List<Course>> GetCourseByCategory(string category)
        {
            Expression<Func<Course, bool>> filter = course => course.Category.CategoryName == category;
            return await repo.Find(filter);
        }

        public async Task Update(Course obj)
        {
            await repo.Update(obj);
        }
    }
}
