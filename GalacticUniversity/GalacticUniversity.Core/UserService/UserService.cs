using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Core.Services;
using GalacticUniversity.DataAccess.UserRepository;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GalacticUniversity.Core.UserService
{
    public class UserService : IUserService<User>
    {
        private readonly IUserRepository<User> _repo;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<User>userManager,IUserRepository<User> repo)
        { 
            _userManager = userManager;
            _repo = repo;
        }
        public async Task Add(User obj)
        {
            await _repo.Add(obj);
        }

        public async Task Delete(User obj)
        {
            await _repo.Delete(obj);
        }

        public async Task<User> Get(string id)
        {
            User user = await _repo.Get(id);
            return user;
        }

        public IQueryable<User> GetAll()
        {
            return _repo.GetAll();
        }

    

        public async Task Update(User obj)
        {
           await _repo.Update(obj);
        }
    }
}
