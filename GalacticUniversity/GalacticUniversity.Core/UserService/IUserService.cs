using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Services;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.UserService
{
    public interface IUserService<User>
    {
        public Task Add(User obj);

        public Task Delete(User obj);

        public Task Update(User obj);

        public IQueryable<User> GetAll();

        public Task<User> Get(string id);
    }
}
