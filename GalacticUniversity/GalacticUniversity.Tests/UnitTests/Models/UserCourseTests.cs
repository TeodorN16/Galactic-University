using System;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    public class UserCoursesTests
    {
        [Test]
        public void UserCourses_Should_Set_Properties_Correctly()
        {
            // Arrange
            var userCourses = new UserCourses
            {
                Id = 1,
                UserID = "user123",
                CourseID = 101,
                LectureID = 1  // Nullable foreign key
            };

            // Assert
            Assert.AreEqual(1, userCourses.Id);
            Assert.AreEqual("user123", userCourses.UserID);
            Assert.AreEqual(101, userCourses.CourseID);
            Assert.AreEqual(1, userCourses.LectureID);
        }

        [Test]
        public void UserCourses_Should_Set_ForeignKey_Relationship_With_User()
        {
            // Arrange
            var user = new User { Id = "user123" };
            var course = new Course { CourseID = 101, CourseName = "Astronomy 101" };
            var lecture = new Lecture { LectureID = 1, LectureName = "The Solar System" };

            var userCourses = new UserCourses
            {
                Id = 1,
                User = user,  // Set the User object
                UserID = user.Id,
                Course = course,  // Set the Course object
                CourseID = course.CourseID,
                Lecture = lecture,  // Set the Lecture object
                LectureID = lecture.LectureID
            };

            // Assert foreign key relationships
            Assert.AreSame(user, userCourses.User);  // Ensure User is set
            Assert.AreEqual("user123", userCourses.UserID);

            Assert.AreSame(course, userCourses.Course);  // Ensure Course is set
            Assert.AreEqual(101, userCourses.CourseID);

            Assert.AreSame(lecture, userCourses.Lecture);  // Ensure Lecture is set
            Assert.AreEqual(1, userCourses.LectureID);
        }

        [Test]
        public void UserCourses_Should_Have_Valid_ForeignKey_Relationships()
        {
            // Arrange
            var userCourses = new UserCourses
            {
                Id = 1,
                UserID = "user123",
                CourseID = 101,
                LectureID = 1  // Nullable foreign key
            };

            // Assert ForeignKey relationships
            Assert.IsNotNull(userCourses.UserID);  // Ensure UserID is set
            Assert.IsNotNull(userCourses.CourseID);  // Ensure CourseID is set
            Assert.IsNotNull(userCourses.LectureID);  // Ensure LectureID is set (even if nullable)
        }

        [Test]
        public void UserCourses_Should_Allow_Nullable_LectureID()
        {
            // Arrange
            var userCourses = new UserCourses
            {
                Id = 1,
                UserID = "user123",
                CourseID = 101,
                LectureID = null  // Nullable foreign key
            };

            // Assert that LectureID can be null
            Assert.IsNull(userCourses.LectureID);
        }
    }
}
