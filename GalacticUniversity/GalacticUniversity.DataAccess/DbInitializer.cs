using CloudinaryDotNet.Actions;
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

            // Initialize roles
            var roles = new List<string> { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Ensure Admin User Exists
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

            // Ensure Regular User Exists
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

            // Seed Categories
            if (!context.categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { CategoryName = "astronomy" },
                    new Category { CategoryName = "astrology" },
                    new Category { CategoryName = "physics" }
                };

                foreach (var category in categories)
                {
                    await context.categories.AddAsync(category);
                }
                await context.SaveChangesAsync();
            }

            // Seed Courses
            if (!context.courses.Any())
            {
                var courses = new Course[]
                {
                    new Course
                    {
                        CourseName = "Astrology Course",
                        Description = "A comprehensive course on the history, practices, and systems of astrology.",
                        StartDate = new DateTime(2023, 1, 1),
                        EndDate = new DateTime(2023, 12, 31),
                        CategoryID = context.categories.FirstOrDefault(c => c.CategoryName == "astrology")?.CategoryID ?? 1,
                        ImageURL = "/images/courses/astrology.jpg"
                    },
                    new Course
                    {
                        CourseName = "Astronomy Course: Cosmic Explorations",
                        Description = "Explore the universe, from neighboring planets to distant galaxies.",
                        StartDate = new DateTime(2023, 2, 1),
                        EndDate = new DateTime(2023, 12, 31),
                        CategoryID = context.categories.FirstOrDefault(c => c.CategoryName == "astronomy")?.CategoryID ?? 1,
                        ImageURL = "/images/courses/astronomy.jpg"
                    },
                    new Course
                    {
                        CourseName = "Physics Course: Everyday Physics Understanding",
                        Description = "Understanding the physics principles that govern our daily lives.",
                        StartDate = new DateTime(2023, 3, 1),
                        EndDate = new DateTime(2023, 12, 31),
                        CategoryID = context.categories.FirstOrDefault(c => c.CategoryName == "physics")?.CategoryID ?? 3,
                        ImageURL = "/images/courses/physics.jpg"
                    }
                };

                foreach (var course in courses)
                {
                    await context.courses.AddAsync(course);
                }
                await context.SaveChangesAsync();
            }

            // Seed Lectures - Get the CourseIDs from the database
            if (!context.lectures.Any())
            {
                // Get the CourseIDs from the database
                var astrologyCourseId = context.courses.FirstOrDefault(c => c.CourseName == "Astrology Course")?.CourseID ?? 0;
                var astronomyCourseId = context.courses.FirstOrDefault(c => c.CourseName == "Astronomy Course: Cosmic Explorations")?.CourseID ?? 0;
                var physicsCourseId = context.courses.FirstOrDefault(c => c.CourseName == "Physics Course: Everyday Physics Understanding")?.CourseID ?? 0;

                if (astrologyCourseId == 0 || astronomyCourseId == 0 || physicsCourseId == 0)
                {
                    // Log error or throw exception if courses are not found
                    throw new Exception("One or more courses not found in the database.");
                }

                var lectures = new List<Lecture>
                {
                    // Astrology Course Lectures
                    new Lecture
                    {
                        LectureName = "History and Cultural Significance of Astrology",
                        Description = "Explores the historical and cultural origins of astrology.",
                        CourseID = astrologyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Houses and Aspects",
                        Description = "Learn about astrological houses and aspects in chart reading.",
                        CourseID = astrologyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Modern Astrological Practices",
                        Description = "Covers modern applications and interpretations of astrology.",
                        CourseID = astrologyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Planets and Their Meanings",
                        Description = "Understand the significance of planetary positions in astrology.",
                        CourseID = astrologyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "The Zodiac System",
                        Description = "Introduces the zodiac system and its components.",
                        CourseID = astrologyCourseId
                    },

                    // Astronomy Course Lectures
                    new Lecture
                    {
                        LectureName = "Exoplanets and the Search for Life",
                        Description = "Exploring planets beyond our solar system and the potential for extraterrestrial life.",
                        CourseID = astronomyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Galaxies and Cosmic Structure",
                        Description = "Understanding galaxy formation and the large-scale structure of the universe.",
                        CourseID = astronomyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Space Exploration History",
                        Description = "The journey of human space exploration from early missions to current endeavors.",
                        CourseID = astronomyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Stars and Their Life Cycles",
                        Description = "Understanding stellar evolution from formation to final stages.",
                        CourseID = astronomyCourseId
                    },
                    new Lecture
                    {
                        LectureName = "The Solar System - Our Cosmic Neighborhood",
                        Description = "Exploring the planets, moons, and other objects in our solar system.",
                        CourseID = astronomyCourseId
                    },

                    // Physics Course Lectures
                    new Lecture
                    {
                        LectureName = "Energy in Our World",
                        Description = "Understanding different forms of energy and their transformations.",
                        CourseID = physicsCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Forces and Motion",
                        Description = "Exploring Newton's laws and the fundamental forces of nature.",
                        CourseID = physicsCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Matter and Materials",
                        Description = "Investigating the properties and behaviors of different materials.",
                        CourseID = physicsCourseId
                    },
                    new Lecture
                    {
                        LectureName = "Waves and Light",
                        Description = "Understanding wave phenomena and the electromagnetic spectrum.",
                        CourseID = physicsCourseId
                    }
                };

                foreach (var lecture in lectures)
                {
                    await context.lectures.AddAsync(lecture);
                }
                await context.SaveChangesAsync();

                // Verify that lectures were added successfully
                if (!context.lectures.Any())
                {
                    throw new Exception("Failed to add lectures to the database.");
                }
            }

            // Seed Lecture Resources
            if (!context.lectureResources.Any())
            {
                // Dictionary to map lecture names to their IDs
                var lectureIds = context.lectures.ToDictionary(l => l.LectureName, l => l.LectureID);

                // Only proceed if we have lectures in the database
                if (lectureIds.Count > 0)
                {
                    var lectureResources = new List<LectureResource>();

                    // Helper method to add resources safely with URL encoding
                    void AddResource(string lectureName, string fileUrl)
                    {
                        if (lectureIds.ContainsKey(lectureName))
                        {
                            // Manually encode the URL: replace spaces with %20
                            string encodedUrl = fileUrl.Replace(" ", "%20");

                            lectureResources.Add(new LectureResource
                            {
                                LectureID = lectureIds[lectureName],
                                FileUrl = encodedUrl
                            });
                        }
                    }

                    // Astrology Course - History and Cultural Significance of Astrology
                    AddResource("History and Cultural Significance of Astrology", "/AstrologyCourse/History%20and%20Cultural%20Significance%20of%20Astrology/ANCIENT%20STARGAZERS.pdf");
                    AddResource("History and Cultural Significance of Astrology", "/AstrologyCourse/History%20and%20Cultural%20Significance%20of%20Astrology/ASTROLOGY%20VS%20ASTRONOMY.pdf");

                    // Astrology Course - Houses and Aspects
                    AddResource("Houses and Aspects", "/AstrologyCourse/Houses%20and%20Aspects/PLANETARY%20RELATIONSHIPS.pdf");
                    AddResource("Houses and Aspects", "/AstrologyCourse/Houses%20and%20Aspects/THE%2012%20HOUSES.pdf");

                    // Astrology Course - Modern Astrological Practices
                    AddResource("Modern Astrological Practices", "/AstrologyCourse/Modern%20Astrological%20Practices/ASTROLOGY%20IN%20POPULAR%20CULTURE.pdf");
                    AddResource("Modern Astrological Practices", "/AstrologyCourse/Modern%20Astrological%20Practices/BEYOND%20SUN%20SIGNS.pdf");

                    // Astrology Course - Planets and Their Meanings
                    AddResource("Planets and Their Meanings", "/AstrologyCourse/Planets%20and%20Their%20Meanings/PLANETARY%20INFLUENCES.pdf");
                    AddResource("Planets and Their Meanings", "/AstrologyCourse/Planets%20and%20Their%20Meanings/READING%20A%20BIRTH%20CHART.pdf");

                    // Astrology Course - The Zodiac System
                    AddResource("The Zodiac System", "/AstrologyCourse/The%20Zodiac%20System/THE%20FOUR%20ELEMENTS.pdf");
                    AddResource("The Zodiac System", "/AstrologyCourse/The%20Zodiac%20System/UNDERSTANDING%20THE%2012%20SIGNS.pdf");

                    // Astronomy Course - Exoplanets and the Search for Life
                    AddResource("Exoplanets and the Search for Life", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Exoplanets%20and%20the%20Search%20for%20Life/COULD%20WE%20BE%20ALONE.pdf");
                    AddResource("Exoplanets and the Search for Life", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Exoplanets%20and%20the%20Search%20for%20Life/DISCOVERING%20NEW%20WORLDS.pdf");

                    // Astronomy Course - Galaxies and Cosmic Structure
                    AddResource("Galaxies and Cosmic Structure", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Galaxies%20and%20Cosmic%20Structure/GALAXY%20TYPES%20AND%20OUR%20MILKY%20WAY.pdf");
                    AddResource("Galaxies and Cosmic Structure", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Galaxies%20and%20Cosmic%20Structure/THE%20EXPANDING%20UNIVERSE.pdf");

                    // Astronomy Course - Space Exploration History
                    AddResource("Space Exploration History", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Space%20Exploration%20History/FROM%20SPUTNIK%20TO%20WEBB.pdf");
                    AddResource("Space Exploration History", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Space%20Exploration%20History/FUTURE%20OF%20SPACE%20TRAVEL.pdf");

                    // Astronomy Course - Stars and Their Life Cycles
                    AddResource("Stars and Their Life Cycles", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Stars%20and%20Their%20Life%20Cycles/HOW%20TO%20STARGAZE.pdf");
                    AddResource("Stars and Their Life Cycles", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/Stars%20and%20Their%20Life%20Cycles/STAR%20BIRTH%20AND%20DEATH.pdf");

                    // Astronomy Course - The Solar System
                    AddResource("The Solar System - Our Cosmic Neighborhood", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/The%20Solar%20System%20-%20Our%20Cosmic%20Neighborhood/THE%20SUN.pdf");
                    AddResource("The Solar System - Our Cosmic Neighborhood", "/ASTRONOMY%20COURSE%20Cosmic%20Explorations/The%20Solar%20System%20-%20Our%20Cosmic%20Neighborhood/TOUR%20OF%20THE%20PLANETS.pdf");

                    // Physics Course - Energy in Our World
                    AddResource("Energy in Our World", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Energy%20in%20Our%20World/ENERGY%20TRANSFORMATIONS.pdf");
                    AddResource("Energy in Our World", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Energy%20in%20Our%20World/RENEWABLE%20ENERGY%20BASICS.pdf");

                    // Physics Course - Forces and Motion
                    AddResource("Forces and Motion", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Forces%20and%20Motion/GRAVITY.pdf");
                    AddResource("Forces and Motion", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Forces%20and%20Motion/NEWTON.pdf");

                    // Physics Course - Matter and Materials
                    AddResource("Matter and Materials", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Matter%20and%20Materials/AMAZING%20MATERIALS.pdf");
                    AddResource("Matter and Materials", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Matter%20and%20Materials/STATES%20OF%20MATTER.pdf");

                    // Physics Course - Waves and Light
                    AddResource("Waves and Light", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Waves%20and%20Light/SOUND%20AND%20MUSIC.pdf");
                    AddResource("Waves and Light", "/PHYSICS%20COURSE%20Everyday%20Physics%20Understanding/Waves%20and%20Light/THE%20ELECTROMAGNETIC%20SPECTRUM.pdf");

                    foreach (var resource in lectureResources)
                    {
                        await context.lectureResources.AddAsync(resource);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }

                
    }
}
    
