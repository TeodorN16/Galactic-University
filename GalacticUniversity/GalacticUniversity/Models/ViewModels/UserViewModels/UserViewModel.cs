namespace GalacticUniversity.Models.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public List<Course> Courses { get; set; }
    }
}
