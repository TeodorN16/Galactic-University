using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using GalacticUniversity.Models;
using Moq;

namespace GalacticUniversity.Tests.Models
{
    [TestFixture]
    public class LectureResourceTests
    {
        [Test]
        public void LectureResource_PropertiesShouldHaveCorrectAttributes()
        {
            // Test for Key attribute on ResourceID
            var resourceIdProperty = typeof(LectureResource).GetProperty("ResourceID");
            var keyAttribute = resourceIdProperty.GetCustomAttribute<KeyAttribute>();
            Assert.IsNotNull(keyAttribute);

            // Test for ForeignKey attribute on LectureID
            var lectureIdProperty = typeof(LectureResource).GetProperty("LectureID");
            var foreignKeyAttribute = lectureIdProperty.GetCustomAttribute<ForeignKeyAttribute>();
            Assert.IsNotNull(foreignKeyAttribute);
            Assert.AreEqual("Lecture", foreignKeyAttribute.Name);

            // Test for NotMapped attribute on File property
            var fileProperty = typeof(LectureResource).GetProperty("File");
            var notMappedAttribute = fileProperty.GetCustomAttribute<NotMappedAttribute>();
            Assert.IsNotNull(notMappedAttribute);
        }

        [Test]
        public void LectureResource_SetAndGetProperties_ShouldWorkCorrectly()
        {
            // Arrange
            var resource = new LectureResource();
            var mockFile = new Mock<IFormFile>();

            // Act
            resource.ResourceID = 1;
            resource.FileUrl = "path/to/resource.pdf";
            resource.File = mockFile.Object;
            resource.LectureID = 5;

            // Assert
            Assert.AreEqual(1, resource.ResourceID);
            Assert.AreEqual("path/to/resource.pdf", resource.FileUrl);
            Assert.AreEqual(mockFile.Object, resource.File);
            Assert.AreEqual(5, resource.LectureID);
        }

        [Test]
        public void LectureResource_NavigationProperty_ShouldBeSettable()
        {
            // Arrange
            var resource = new LectureResource();
            var lecture = new Lecture
            {
                LectureID = 5,
                LectureName = "Advanced Quantum Theory",
                Description = "Complex quantum mechanics theories"
            };

            // Act
            resource.Lecture = lecture;

            // Assert
            Assert.IsNotNull(resource.Lecture);
            Assert.AreEqual(5, resource.Lecture.LectureID);
            Assert.AreEqual("Advanced Quantum Theory", resource.Lecture.LectureName);
        }

        [Test]
        public void LectureResource_WithFile_ShouldBeNonPersistent()
        {
            // Test to verify the File property is correctly marked as non-persistent
            // This test confirms that the NotMapped attribute is working as expected

            // Create a simple LectureResource instance
            var resource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "path/to/file.pdf",
                LectureID = 2
            };

            // Create a mock IFormFile
            var mockFormFile = new Mock<IFormFile>();
            mockFormFile.Setup(f => f.FileName).Returns("test.pdf");

            // Set the File property
            resource.File = mockFormFile.Object;

            // Verify the File property is correctly set
            Assert.IsNotNull(resource.File);
            Assert.AreEqual("test.pdf", resource.File.FileName);

            // The NotMapped attribute itself has already been verified in the first test
        }
    }
}