using System;
using System.Collections.Generic;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    public class LectureTests
    {
        [Test]
        public void Lecture_Should_Set_Properties_Correctly()
        {
            // Arrange
            var lecture = new Lecture
            {
                LectureID = 1,
                LectureName = "The Solar System",
                Description = "Introduction to the solar system",
                CourseID = 101
            };

            // Assert
            Assert.AreEqual(1, lecture.LectureID);
            Assert.AreEqual("The Solar System", lecture.LectureName);
            Assert.AreEqual("Introduction to the solar system", lecture.Description);
            Assert.AreEqual(101, lecture.CourseID);
        }

        [Test]
        public void Lecture_Should_Allow_Adding_LectureResources()
        {
            // Arrange
            var lecture = new Lecture
            {
                LectureID = 1,
                LectureName = "The Solar System",
                Description = "Introduction to the solar system",
                CourseID = 101,
                LectureResources = new List<LectureResource>()
            };

            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "https://example.com/solar-system.pdf",
                LectureID = 1
            };

            // Act
            lecture.LectureResources.Add(lectureResource);

            // Assert
            Assert.AreEqual(1, lecture.LectureResources.Count);
            Assert.AreSame(lectureResource, lecture.LectureResources.First());
            Assert.AreEqual(1, lectureResource.LectureID); // Ensure LectureID is set correctly
        }
    }
}
