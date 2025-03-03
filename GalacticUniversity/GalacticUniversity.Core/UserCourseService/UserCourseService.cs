using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;



namespace GalacticUniversity.Core.UserCourseService
{
    public class UserCourseService : IUserCourseService
    {
        private readonly IRepository<UserCourses> _repo;
        private readonly IRepository<Course> _courseRepo;

        public UserCourseService(IRepository<UserCourses> repo, IRepository<Course> courseRepo)
        { 
            _repo = repo;
            _courseRepo = courseRepo;
        }
        public async Task Add(UserCourses obj)
        {
           await _repo.Add(obj);
        }

        public async Task Delete(UserCourses obj)
        {
           await _repo.Delete(obj);
        }

        public async Task<UserCourses> Get(int id)
        {
            UserCourses userCourses = await _repo.Get(id);
            return userCourses;
        }

        public  IQueryable<UserCourses> GetAll()
        {
           return _repo.GetAll();
        }

        public async Task<bool> JoinCourse(string userId, int courseId)
        {
            var check = _repo.GetAll().FirstOrDefault(us => us.UserID == userId && us.CourseID == courseId);
            if (check != null)
            {
                return false;
            }
            
                var userCourse = new UserCourses
                {
                    UserID = userId,
                    CourseID = courseId,
                };

            await _repo.Add(userCourse);
            return true;
        }

        public async Task<bool> LeaveCourse(string userId, int courseId)
        {
            var courseExists = _courseRepo.GetAll().Any(c => c.CourseID == courseId);

            if (courseExists==false)
            {
                return false;
            }

            var check = _repo.GetAll().FirstOrDefault(us => us.UserID == userId && us.CourseID == courseId);
            if (check == null)
            {
                return false;
            }

            await _repo.Delete(check);
            
            return true;
        }

        public async Task Update(UserCourses obj)
        {
            await _repo.Update(obj);
        }
       

       
    }
}
