using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.UserService;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalacticUniversity.Controllers
{
    public class UserController : Controller
    {
        private readonly UserCourseService _userCourseService;
        private readonly UserService _userService;

        public UserController(UserCourseService userCourseService,UserService userService)
        { 
            _userCourseService = userCourseService;
            _userService = userService;
        }
        public  IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> MyProfile(string userId)
        {
            User user = await _userService.Get(userId);
            return View(user);    
        }
       

       

    }
}
