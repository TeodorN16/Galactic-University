using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Services;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.UserService
{
    public interface IUserService:IService<User>
    {
        Task<User> GetUserID();
    }
}
