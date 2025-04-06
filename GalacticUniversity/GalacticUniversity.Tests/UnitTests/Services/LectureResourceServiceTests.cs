using Moq;
using NUnit.Framework;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Core.LectureResourceService;
using GalacticUniversity.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace GalacticUniversity.Tests.UnitTests
{
    [TestFixture]
    public class LectureResourceServiceTests
    {
        private Mock<IRepository<LectureResource>> _mockRepository;
        private LectureResourceService _lectureResourceService;

        [SetUp]
        public void Setup()
        {
            // Create the mock repository for LectureResource
            _mockRepository = new Mock<IRepository<LectureResource>>();

            // Pass the mock repository to the LectureResourceService constructor
            _lectureResourceService = new LectureResourceService(_mockRepository.Object);
        }

        [Test]
        public async Task Add_ShouldCallRepositoryAdd()
        {
            // Arrange
            var mockFormFile = new Mock<IFormFile>();
            mockFormFile.Setup(f => f.FileName).Returns("test.pdf");

            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "path/to/resource.pdf",
                LectureID = 1,
                File = mockFormFile.Object
            };

            _mockRepository.Setup(r => r.Add(It.IsAny<LectureResource>())).Returns(Task.CompletedTask);

            // Act
            await _lectureResourceService.Add(lectureResource);

            // Assert
            _mockRepository.Verify(r => r.Add(It.Is<LectureResource>(lr =>
                lr.FileUrl == "path/to/resource.pdf" &&
                lr.LectureID == 1)), Times.Once);
        }

        [Test]
        public async Task Delete_ShouldCallRepositoryDelete()
        {
            // Arrange
            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "path/to/delete.pdf",
                LectureID = 2
            };

            _mockRepository.Setup(r => r.Delete(It.IsAny<LectureResource>())).Returns(Task.CompletedTask);

            // Act
            await _lectureResourceService.Delete(lectureResource);

            // Assert
            _mockRepository.Verify(r => r.Delete(It.Is<LectureResource>(lr =>
                lr.FileUrl == "path/to/delete.pdf" &&
                lr.LectureID == 2)), Times.Once);
        }

        [Test]
        public async Task Get_ShouldCallRepositoryGet()
        {
            // Arrange
            var lecture = new Lecture
            {
                LectureID = 5,
                LectureName = "Advanced Physics"
            };

            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "path/to/physics.pdf",
                LectureID = 5,
                Lecture = lecture
            };

            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(lectureResource);

            // Act
            var result = await _lectureResourceService.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("path/to/physics.pdf", result.FileUrl);
            Assert.AreEqual(5, result.LectureID);
            Assert.IsNotNull(result.Lecture);
            Assert.AreEqual("Advanced Physics", result.Lecture.LectureName);
            _mockRepository.Verify(r => r.Get(1), Times.Once);
        }

        [Test]
        public async Task Get_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((LectureResource)null);

            // Act
            var result = await _lectureResourceService.Get(999);

            // Assert
            Assert.IsNull(result);
            _mockRepository.Verify(r => r.Get(999), Times.Once);
        }

        [Test]
        public void GetAll_ShouldCallRepositoryGetAll()
        {
            // Arrange
            var resources = new List<LectureResource>
            {
                new LectureResource {
                    ResourceID = 1,
                    FileUrl = "path/to/resource1.pdf",
                    LectureID = 1
                },
                new LectureResource {
                    ResourceID = 2,
                    FileUrl = "path/to/resource2.mp4",
                    LectureID = 1
                },
                new LectureResource {
                    ResourceID = 3,
                    FileUrl = "path/to/resource3.pptx",
                    LectureID = 2
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(resources);

            // Act
            var result = _lectureResourceService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("path/to/resource1.pdf", result.First().FileUrl);
            Assert.AreEqual("path/to/resource3.pptx", result.Last().FileUrl);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public async Task Update_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var mockFormFile = new Mock<IFormFile>();
            mockFormFile.Setup(f => f.FileName).Returns("updated.pdf");

            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "path/to/updated-resource.pdf",
                LectureID = 3,
                File = mockFormFile.Object
            };

            _mockRepository.Setup(r => r.Update(It.IsAny<LectureResource>())).Returns(Task.CompletedTask);

            // Act
            await _lectureResourceService.Update(lectureResource);

            // Assert
            _mockRepository.Verify(r => r.Update(It.Is<LectureResource>(lr =>
                lr.FileUrl == "path/to/updated-resource.pdf" &&
                lr.LectureID == 3)), Times.Once);
        }

        [Test]
        public void GetAll_FilterByLectureID_ShouldReturnFilteredResults()
        {
            // Arrange
            var resources = new List<LectureResource>
            {
                new LectureResource {
                    ResourceID = 1,
                    FileUrl = "path/to/lecture1/resource1.pdf",
                    LectureID = 1
                },
                new LectureResource {
                    ResourceID = 2,
                    FileUrl = "path/to/lecture1/resource2.pdf",
                    LectureID = 1
                },
                new LectureResource {
                    ResourceID = 3,
                    FileUrl = "path/to/lecture2/resource1.pdf",
                    LectureID = 2
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(resources);

            // Act
            var result = _lectureResourceService.GetAll().Where(r => r.LectureID == 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().LectureID);
            Assert.AreEqual(1, result.Last().LectureID);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public async Task Get_WithRelatedData_ShouldIncludeNavigationProperties()
        {
            // Arrange
            var course = new Course { CourseID = 10, CourseName = "Advanced Physics" };

            var lecture = new Lecture
            {
                LectureID = 5,
                LectureName = "Quantum Theory",
                Description = "Introduction to quantum mechanics",
                CourseID = 10,
                Course = course
            };

            var lectureResource = new LectureResource
            {
                ResourceID = 1,
                FileUrl = "path/to/quantum.pdf",
                LectureID = 5,
                Lecture = lecture
            };

            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(lectureResource);

            // Act
            var result = await _lectureResourceService.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Lecture);
            Assert.IsNotNull(result.Lecture.Course);
            Assert.AreEqual("Quantum Theory", result.Lecture.LectureName);
            Assert.AreEqual("Advanced Physics", result.Lecture.Course.CourseName);
        }
    }
}