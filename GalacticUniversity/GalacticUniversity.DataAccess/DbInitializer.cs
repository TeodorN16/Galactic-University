using GalacticUniversity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GalacticUniversity.DataAccess
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            var roles = new List<string> { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            //Ensure Admin User Exists
            string adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                User user = new User
                {
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    PhoneNumber = "0894712253",
                    EmailConfirmed = true
                };
                var createResult = await userManager.CreateAsync(user, "@dmIn25");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            string userEmail = "tina@gmail.com";
            var userUser = await userManager.FindByEmailAsync(userEmail);
            if (userUser == null)
            {
                var user1 = new User
                {
                    UserName = "Tina",
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpper(),
                    NormalizedUserName = "TINA",
                    PhoneNumber = "0899993243",
                    EmailConfirmed = true
                };
                var createResult = await userManager.CreateAsync(user1, "tiN@0641");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "User");
                }
            }
            // Remove the duplicate Database.EnsureCreated()
            if (!context.categories.Any())
            {
                var categories = new Category[]
                    {
                        new Category
                        {
                            CategoryName="astronomy"
                        },
                        new Category
                        {
                            CategoryName="astrology"
                        },
                        new Category
                        {
                            CategoryName="physics"
                        },
                    };
                foreach (var category in categories)
                {
                    await context.categories.AddAsync(category);
                }
                await context.SaveChangesAsync();
            }
            //if (!context.lectureResources.Any())
            //{
            //    var lectureResources = new LectureResource[]
            //    {
            //        new LectureResource
            //        {
            //            LectureID=1,
            //            //FileUrl
            //        },
            //        new LectureResource
            //        {
            //            LectureID = 1,
            //        },
            //        new LectureResource
            //        {
            //            LectureID=1,
            //        },
            //    };
            //    foreach (var lectureResource in lectureResources)
            //    {
            //        await context.lectureResources.AddAsync(lectureResource);
            //    }
            //    await context.SaveChangesAsync();
            //}
        }
    }
}