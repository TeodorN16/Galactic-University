using Moq;
using NUnit.Framework;
using GalacticUniversity.Core.Services;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using System.Threading.Tasks;

namespace GalacticUniversity.Tests.UnitTests
{
    [TestFixture]
    public class ServiceTests
    {
        private Mock<IRepository<Category>> _mockRepository;
        private Service<Category> _service;

        [SetUp]
        public void Setup()
        {
            // Create the mock repository for Category
            _mockRepository = new Mock<IRepository<Category>>();

            // Setup mock methods (e.g., Add, Delete, Get)
            _mockRepository.Setup(r => r.Add(It.IsAny<Category>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.Delete(It.IsAny<Category>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Category { CategoryID = 1, CategoryName = "Test Category" });
            _mockRepository.Setup(r => r.Update(It.IsAny<Category>())).Returns(Task.CompletedTask);

            // Pass the mock repository to the Service<Category> constructor
            _service = new Service<Category>(_mockRepository.Object);
        }

        [Test]
        public async Task Add_ShouldCallRepositoryAdd()
        {
            // Arrange
            var category = new Category { CategoryID = 1, CategoryName = "Physics" };
            _mockRepository.Setup(r => r.Add(It.IsAny<Category>())).Returns(Task.CompletedTask);

            // Act
            await _service.Add(category);

            // Assert
            _mockRepository.Verify(r => r.Add(It.Is<Category>(c => c.CategoryName == "Physics")), Times.Once);
        }

        [Test]
        public async Task Delete_ShouldCallRepositoryDelete()
        {
            // Arrange
            var category = new Category { CategoryID = 1, CategoryName = "Chemistry" };
            _mockRepository.Setup(r => r.Delete(It.IsAny<Category>())).Returns(Task.CompletedTask);

            // Act
            await _service.Delete(category);

            // Assert
            _mockRepository.Verify(r => r.Delete(It.Is<Category>(c => c.CategoryName == "Chemistry")), Times.Once);
        }

        [Test]
        public async Task Get_ShouldCallRepositoryGet()
        {
            // Arrange
            var category = new Category { CategoryID = 1, CategoryName = "Mathematics" };
            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(category);

            // Act
            var result = await _service.Get(1);

            // Assert
            Assert.AreEqual("Mathematics", result.CategoryName);
            _mockRepository.Verify(r => r.Get(1), Times.Once);
        }

        [Test]
        public async Task Update_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var category = new Category { CategoryID = 1, CategoryName = "History" };
            _mockRepository.Setup(r => r.Update(It.IsAny<Category>())).Returns(Task.CompletedTask);

            // Act
            await _service.Update(category);

            // Assert
            _mockRepository.Verify(r => r.Update(It.Is<Category>(c => c.CategoryName == "History")), Times.Once);
        }
    }
}