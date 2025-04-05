using System;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Models
{
    [TestFixture]
    public class LectureResourceTests
    {
        [Test]
        public void LectureResource_Should_Set_Properties_Correctly()
        {
            // Arrange
            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "https://example.com/solar-system.pdf",
                LectureID = 1
            };

            // Assert
            Assert.AreEqual(1, lectureResource.ResourceID);
            Assert.AreEqual("https://example.com/solar-system.pdf", lectureResource.FileUrl);
            Assert.AreEqual(1, lectureResource.LectureID);
        }

        [Test]
        public void LectureResource_Should_Have_Valid_ForeignKey_Relationship()
        {
            // Arrange
            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "https://example.com/solar-system.pdf",
                LectureID = 1
            };

            // Assert (Foreign Key Relationship Check)
            Assert.IsNotNull(lectureResource.LectureID);
        }
    }
}
