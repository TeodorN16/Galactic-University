using System;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    public class CommentTests
    {
        [Test]
        public void Comment_Should_Set_Properties_Correctly()
        {
            // Arrange
            var comment = new Comment
            {
                CommentID = 1,
                CommentText = "Great course!",
                CommentDate = DateTime.UtcNow,
                Rating = 5,
                UserID = "user123",
                CourseID = 101
            };

            // Assert
            Assert.AreEqual(1, comment.CommentID);
            Assert.AreEqual("Great course!", comment.CommentText);
            Assert.AreEqual(5, comment.Rating);
            Assert.AreEqual("user123", comment.UserID);
            Assert.AreEqual(101, comment.CourseID);
            Assert.AreEqual(typeof(DateTime), comment.CommentDate.GetType());
        }

        [Test]
        public void Comment_Should_Have_Valid_Foreign_Key_Relationships()
        {
            // Arrange
            var comment = new Comment
            {
                CommentID = 1,
                CommentText = "Great course!",
                CommentDate = DateTime.UtcNow,
                Rating = 5,
                UserID = "user123",
                CourseID = 101
            };

            // Assert (Foreign key relationships check)
            Assert.IsNotNull(comment.UserID);
            Assert.IsNotNull(comment.CourseID);
        }
    }
}
