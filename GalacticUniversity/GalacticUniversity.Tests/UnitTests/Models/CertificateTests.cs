using System;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    public class CertificateTests
    {
        [Test]
        public void Certificate_Should_Set_Properties_Correctly()
        {
            // Arrange
            var certificate = new Certificate
            {
                CertificateID = 1,
                UserID = "user123",
                CourseID = 101,
                IssueDate = DateTime.Today,
                CertificateUrl = "https://cloudinary.com/xyz123"
            };

            // Assert
            Assert.AreEqual(1, certificate.CertificateID);
            Assert.AreEqual("user123", certificate.UserID);
            Assert.AreEqual(101, certificate.CourseID);
            Assert.AreEqual(DateTime.Today, certificate.IssueDate.Date);
            Assert.AreEqual("https://cloudinary.com/xyz123", certificate.CertificateUrl);
        }

        [Test]
        public void Certificate_Should_Set_ForeignKey_Relationship_With_User()
        {
            // Arrange
            var user = new User { Id = "user123" };
            var course = new Course { CourseID = 101, CourseName = "Astronomy 101" };

            var certificate = new Certificate
            {
                CertificateID = 1,
                User = user,  // Set the User object
                UserID = user.Id,
                Course = course,  // Set the Course object
                CourseID = course.CourseID,
                IssueDate = DateTime.Today,
                CertificateUrl = "https://cloudinary.com/xyz123"
            };

            // Assert the foreign key relationship
            Assert.AreSame(user, certificate.User);  // Ensure the User object is properly assigned
            Assert.AreEqual("user123", certificate.UserID);
            Assert.AreSame(course, certificate.Course);  // Ensure the Course object is properly assigned
            Assert.AreEqual(101, certificate.CourseID);
        }

        [Test]
        public void Certificate_Should_Have_Valid_ForeignKey_Relationships()
        {
            // Arrange
            var certificate = new Certificate
            {
                CertificateID = 1,
                UserID = "user123",
                CourseID = 101,
                IssueDate = DateTime.Today,
                CertificateUrl = "https://cloudinary.com/xyz123"
            };

            // Assert ForeignKey relationships
            Assert.IsNotNull(certificate.UserID);  // Ensure UserID is set
            Assert.IsNotNull(certificate.CourseID);  // Ensure CourseID is set
        }
    }
}
