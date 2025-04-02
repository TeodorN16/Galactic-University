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

                    // Helper method to add resources safely
                    void AddResource(string lectureName, string fileUrl)
                    {
                        if (lectureIds.ContainsKey(lectureName))
                        {
                            lectureResources.Add(new LectureResource
                            {
                                LectureID = lectureIds[lectureName],
                                FileUrl = fileUrl
                            });
                        }
                    }

                    // Astrology Course - History and Cultural Significance of Astrology
                    AddResource("History and Cultural Significance of Astrology", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581065/ANCIENT_STARGAZERS_vin8hw.pdf");
                    AddResource("History and Cultural Significance of Astrology", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581209/ASTROLOGY_VS_ASTRONOMY_kkrcs9.pdf");

                    // Astrology Course - Houses and Aspects
                    AddResource("Houses and Aspects", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581250/PLANETARY_RELATIONSHIPS_sosola.pdf");
                    AddResource("Houses and Aspects", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581284/THE_12_HOUSES_c6fa46.pdf");

                    // Astrology Course - Modern Astrological Practices
                    AddResource("Modern Astrological Practices", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581316/ASTROLOGY_IN_POPULAR_CULTURE_o3jr04.pdf");
                    AddResource("Modern Astrological Practices", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581318/BEYOND_SUN_SIGNS_w0bgtb.pdf");

                    // Astrology Course - Planets and Their Meanings
                    AddResource("Planets and Their Meanings", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581384/PLANETARY_INFLUENCES_iqhrcd.pdf");
                    AddResource("Planets and Their Meanings", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581389/READING_A_BIRTH_CHART_k54fkt.pdf");

                    // Astrology Course - The Zodiac System
                    AddResource("The Zodiac System", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581585/THE_FOUR_ELEMENTS_ouphve.pdf");
                    AddResource("The Zodiac System", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581587/UNDERSTANDING_THE_12_SIGNS_ccsdvo.pdf");

                    // Astronomy Course - Exoplanets and the Search for Life
                    AddResource("Exoplanets and the Search for Life", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581643/COULD_WE_BE_ALONE_ktvwof.pdf");
                    AddResource("Exoplanets and the Search for Life", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581644/DISCOVERING_NEW_WORLDS_cevh8x.pdf");

                    // Astronomy Course - Galaxies and Cosmic Structure
                    AddResource("Galaxies and Cosmic Structure", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581697/GALAXY_TYPES_AND_OUR_MILKY_WAY_uxtoch.pdf");
                    AddResource("Galaxies and Cosmic Structure", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581699/THE_EXPANDING_UNIVERSE_z8l3me.pdf");

                    // Astronomy Course - Space Exploration History
                    AddResource("Space Exploration History", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581846/FROM_SPUTNIK_TO_WEBB_pvxtkp.pdf");
                    AddResource("Space Exploration History", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581848/FUTURE_OF_SPACE_TRAVEL_cvaqmm.pdf");

                    // Astronomy Course - Stars and Their Life Cycles
                    AddResource("Stars and Their Life Cycles", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581857/HOW_TO_STARGAZE_ezmoha.pdf");
                    AddResource("Stars and Their Life Cycles", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581863/STAR_BIRTH_AND_DEATH_zal7es.pdff");

                    // Astronomy Course - The Solar System
                    AddResource("The Solar System - Our Cosmic Neighborhood", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581870/THE_SUN_ofvro1.pdf");
                    AddResource("The Solar System - Our Cosmic Neighborhood", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581873/TOUR_OF_THE_PLANETS_kwhl6k.pdf");

                    // Physics Course - Energy in Our World
                    AddResource("Energy in Our World", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581843/ENERGY_TRANSFORMATIONS_ogsjox.pdf");
                    AddResource("Energy in Our World", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581857/RENEWABLE_ENERGY_BASICS_uuv3he.pdf");

                    // Physics Course - Forces and Motion
                    AddResource("Forces and Motion", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581852/GRAVITY_wq7ojo.pdf");
                    AddResource("Forces and Motion", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581856/NEWTON_akfkrb.pdf");

                    // Physics Course - Matter and Materials
                    AddResource("Matter and Materials", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581842/AMAZING_MATERIALS_a7q9n7.pdf");
                    AddResource("Matter and Materials", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581864/STATES_OF_MATTER_wq7m6j.pdf");

                    // Physics Course - Waves and Light
                    AddResource("Waves and Light", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581860/SOUND_AND_MUSIC_p91gti.pdf");
                    AddResource("Waves and Light", "https://res.cloudinary.com/ddyzobtms/image/upload/v1743581866/THE_ELECTROMAGNETIC_SPECTRUM_gacjly.pdf");

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