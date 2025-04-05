using System;
using System.Collections.Generic;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    public class CourseTests
    {
        [Test]
        public void Course_Should_Set_Properties_Correctly()
        {
            // Arrange
            var course = new Course
            {
                CourseID = 101,
                CourseName = "Introduction to Astronomy",
                Description = "A beginner's course in astronomy",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(3),
                CategoryID = 1
            };

            // Assert
            Assert.AreEqual(101, course.CourseID);
            Assert.AreEqual("Introduction to Astronomy", course.CourseName);
            Assert.AreEqual("A beginner's course in astronomy", course.Description);
            Assert.AreEqual(DateTime.Today, course.StartDate.Date);
            Assert.AreEqual(DateTime.Today.AddMonths(3), course.EndDate.Date);
            Assert.AreEqual(1, course.CategoryID);
        }

        [Test]
        public void Course_Should_Allow_Adding_Lectures()
        {
            // Arrange
            var course = new Course
            {
                CourseID = 101,
                CourseName = "Introduction to Astronomy",
                Description = "A beginner's course in astronomy",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(3),
                CategoryID = 1,
                Lectures = new List<Lecture>()
            };

            var lecture = new Lecture
            {
                LectureID = 1,
                LectureName = "The Solar System",
                Description = "Introduction to the solar system",
                // Link the lecture to the course explicitly
                CourseID = course.CourseID
            };

            // Act
            course.Lectures.Add(lecture);

            // Assert
            Assert.AreEqual(1, course.Lectures.Count);
            Assert.AreSame(lecture, course.Lectures.First());
            Assert.AreEqual(101, lecture.CourseID);  // Ensuring that the CourseID is set correctly
        }

    }
}
