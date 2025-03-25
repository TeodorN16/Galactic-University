using GalacticUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.DataAccess
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        { 
            context.Database.EnsureCreated();
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
            //            ResourceID=1,
            //            LectureID=1,
            //            //FileUrl
            //        },
            //        new LectureResource
            //        {
            //            ResourceID=2,
            //            LectureID = 1,
            //        },
            //        new LectureResource
            //        {
            //            ResourceID=3,
            //            LectureID=1,
            //        },

            //    };
            //    foreach (var lectureResource in lectureResources)
            //    { 
                   
            //    }
            //}
        }
    }
}
