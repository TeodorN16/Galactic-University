using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Core.Services;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;

        public UserService(IRepository<User> repo)
        { 
            _repo = repo;
        }
        public async Task Add(User obj)
        {
            await _repo.Add(obj);
        }

        public async Task Delete(User obj)
        {
            await _repo.Delete(obj]);
        }

        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
