using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.UserService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalacticUniversity.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserCourseService _userCourseService;
        private readonly IUserService<User> _userService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserCourseService userCourseService,IUserService<User> userService, UserManager<User> userManager)
        { 
            _userCourseService = userCourseService;
            _userService = userService;
            _userManager = userManager;
        }
        public  IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> MyProfile(UserViewModel? uvm)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var courses = _userCourseService
                  .GetAll()
                  .Where(u => u.UserID == currentUser.Id)
                  .Include(u => u.Course) // Include the related Course entity
                  .Select(u => u.Course)  // Select the Course data from the UserCourses relationship
                  .ToList();

            var model = new UserViewModel
            {
                Id = currentUser.Id,
                Email = currentUser.Email,
                Courses = courses
            };

            return View(model);    
        }
       

       

    }
}
