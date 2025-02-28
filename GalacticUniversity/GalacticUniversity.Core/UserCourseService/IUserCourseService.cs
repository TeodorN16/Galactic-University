using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Services;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.UserCourseService
{
    public interface IUserCourseService:IService<UserCourses>
    {
        Task<bool> JoinCourse(string userId,int courseId);
        Task<bool> LeaveCourse(string userId,int courseId);
    }
}
