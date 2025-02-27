using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.UserCourseService
{
    internal class UserCourseService : IUserCourseService
    {
        private readonly IRepository<UserCourses> _repo;

        public UserCourseService(IRepository<UserCourses> repo)
        { 
            _repo = repo;
        }
        public async Task Add(UserCourses obj)
        {
           _repo.Add(obj);
        }

        public async Task Delete(UserCourses obj)
        {
            throw new NotImplementedException();
        }

        public async Task<UserCourses> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserCourses> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(UserCourses obj)
        {
            throw new NotImplementedException();
        }
    }
}
