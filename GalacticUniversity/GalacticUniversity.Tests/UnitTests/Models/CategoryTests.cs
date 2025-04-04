using System;
using System.Collections.Generic;
using System.Linq;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    class CategoryTests
    {
        [Test]
        public void Category_Should_Set_Properties_Correctly()
        {
            // Arrange
            var category = new Category
            {
                CategoryID = 1,
                CategoryName = "Astronomy",
                Courses = new List<Course>()
            };

            // Assert
            Assert.AreEqual(1, category.CategoryID);
            Assert.AreEqual("Astronomy", category.CategoryName);
            Assert.IsNotNull(category.Courses);
            Assert.IsEmpty(category.Courses);
        }

        [Test]
        public void Category_Courses_Should_Allow_Adding_Courses()
        {
            // Arrange
            var category = new Category
            {
                CategoryID = 1,
                CategoryName = "Astrology",
                Courses = new List<Course>()
            };

            var course = new Course
            {
                CourseID = 101,
                CourseName = "Beginner Astrology",
                Description = "An intro course",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                CategoryID = 1,
                Category = category
            };

            // Act
            category.Courses.Add(course);

            // Assert
            Assert.AreEqual(1, category.Courses.Count);
            Assert.AreSame(course, category.Courses.First());
            Assert.AreEqual("Beginner Astrology", category.Courses.First().CourseName);
            Assert.AreEqual(category.CategoryID, course.CategoryID);
        }
    }
}
