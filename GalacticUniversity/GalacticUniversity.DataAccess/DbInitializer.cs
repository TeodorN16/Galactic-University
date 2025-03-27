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
            if (!context.courses.Any())
            {
                var course = new Course
                {
                    CourseID = 1,
                    CourseName = "Astrology Course",
                    Description = "A comprehensive course on the history, practices, and systems of astrology.",
                    StartDate = new DateTime(2023, 1, 1),
                    EndDate = new DateTime(2023, 12, 31),
                    CategoryID = 1, // Assuming a category exists
                    ImageURL = null
                };
                await context.courses.AddAsync(course);
                await context.SaveChangesAsync();
            }

            // Seed the Lectures
            if (!context.lectures.Any())
            {
                var lectures = new Lecture[]
                {
            new Lecture
            {
                LectureID = 1,
                LectureName = "History and Cultural Significance of Astrology",
                Description = "Explores the historical and cultural origins of astrology.",
                CourseID = 1
            },
            new Lecture
            {
                LectureID = 2,
                LectureName = "Modern Astrological Practices",
                Description = "Covers modern applications and interpretations of astrology.",
                CourseID = 1
            },
            new Lecture
            {
                LectureID = 3,
                LectureName = "The Zodiac System",
                Description = "Introduces the zodiac system and its components.",
                CourseID = 1
            }
                };

                foreach (var lecture in lectures)
                {
                    await context.lectureResources.AddAsync(lectures);
                }
                await context.SaveChangesAsync();
            }

            // Seed the LectureResources
            if (!context.lectureResources.Any())
            {
                var lectureResources = new LectureResource[]
                {
            // Lecture 1: History and Cultural Significance of Astrology
            new LectureResource
            {
                LectureID = 1,
                FileUrl = "/AstrologyCourse/History and Cultural Significance of Astrology/ANCIENT STARGAZERS.pdf"
            },
            new LectureResource
            {
                LectureID = 1,
                FileUrl = "/AstrologyCourse/History and Cultural Significance of Astrology/ASTROLOGY VS ASTRONOMY.pdf"
            },
            new LectureResource
            {
                LectureID = 1,
                FileUrl = "/AstrologyCourse/History and Cultural Significance of Astrology/HOUSES AND ASPECTS.pdf"
            },
            new LectureResource
            {
                LectureID = 1,
                FileUrl = "/AstrologyCourse/History and Cultural Significance of Astrology/THE 12 HOUSES.pdf"
            },
            // Lecture 2: Modern Astrological Practices
            new LectureResource
            {
                LectureID = 2,
                FileUrl = "/AstrologyCourse/Modern Astrological Practices/ASTROLOGY IN POPULAR CULTURE.pdf"
            },
            new LectureResource
            {
                LectureID = 2,
                FileUrl = "/AstrologyCourse/Modern Astrological Practices/BEYOND SUN SIGNS.pdf"
            },
            new LectureResource
            {
                LectureID = 2,
                FileUrl = "/AstrologyCourse/Modern Astrological Practices/PLANETS AND THEIR MEANINGS.pdf"
            },
            new LectureResource
            {
                LectureID = 2,
                FileUrl = "/AstrologyCourse/Modern Astrological Practices/PLANETARY INFLUENCES.pdf"
            },
            new LectureResource
            {
                LectureID = 2,
                FileUrl = "/AstrologyCourse/Modern Astrological Practices/READING A BIRTH CHART.pdf"
            },
            // Lecture 3: The Zodiac System
            new LectureResource
            {
                LectureID = 3,
                FileUrl = "/AstrologyCourse/The Zodiac System/THE FOUR ELEMENTS.pdf"
            },
            new LectureResource
            {
                LectureID = 3,
                FileUrl = "/AstrologyCourse/The Zodiac System/UNDERSTANDING THE 12 SIGNS.pdf"
            }
                };

                foreach (var lectureResource in lectureResources)
                {
                    await context.lectures.AddAsync(lectureResource);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
