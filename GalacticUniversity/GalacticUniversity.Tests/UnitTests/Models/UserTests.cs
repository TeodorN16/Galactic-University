using System;
using System.Collections.Generic;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void User_Should_Set_Properties_Correctly()
        {
            // Arrange
            var user = new User
            {
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Comments = new List<Comment>(),
                UserCourses = new List<UserCourses>(),
                Certificates = new List<Certificate>()
            };

            // Assert
            Assert.AreEqual("johndoe", user.UserName);
            Assert.AreEqual("johndoe@example.com", user.Email);
        }

        [Test]
        public void User_Should_Allow_Adding_Comments()
        {
            // Arrange
            var user = new User
            {
                UserName = "johndoe",
                Comments = new List<Comment>()
            };

            var comment = new Comment
            {
                CommentID = 1,
                CommentText = "Great course!",
                CommentDate = DateTime.UtcNow,
                Rating = 5,
                UserID = "user123",
                CourseID = 101
            };

            // Act
            user.Comments.Add(comment);

            // Assert
            Assert.AreEqual(1, user.Comments.Count);
            Assert.AreSame(comment, user.Comments.First());
        }

        [Test]
        public void User_Should_Allow_Adding_Courses()
        {
            // Arrange
            var user = new User
            {
                UserName = "johndoe",
                UserCourses = new List<UserCourses>()
            };

            var userCourse = new UserCourses
            {
                UserID = "user123",
                CourseID = 101
            };

            // Act
            user.UserCourses.Add(userCourse);

            // Assert
            Assert.AreEqual(1, user.UserCourses.Count);
            Assert.AreSame(userCourse, user.UserCourses.First());
        }
    }
}

