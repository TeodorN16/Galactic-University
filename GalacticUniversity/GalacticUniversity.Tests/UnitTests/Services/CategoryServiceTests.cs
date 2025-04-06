using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GalacticUniversity.Tests.UnitTests.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IRepository<Category>> _mockRepository;
        private CategoryService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Category>>();
            _service = new CategoryService(_mockRepository.Object);
        }

        [Test]
        public async Task Add_ValidCategory_ShouldCallRepositoryAdd()
        {
            // Arrange
            var category = new Category { CategoryID = 1, CategoryName = "Astronomy" };

            // Act
            await _service.Add(category);

            // Assert
            _mockRepository.Verify(r => r.Add(category), Times.Once);
        }

        [Test]
        public async Task Delete_ValidCategory_ShouldCallRepositoryDelete()
        {
            // Arrange
            var category = new Category { CategoryID = 2, CategoryName = "Astrology" };

            // Act
            await _service.Delete(category);

            // Assert
            _mockRepository.Verify(r => r.Delete(category), Times.Once);
        }

        [Test]
        public async Task Get_ValidId_ShouldReturnCategory()
        {
            // Arrange
            var category = new Category { CategoryID = 3, CategoryName = "Physics" };
            _mockRepository.Setup(r => r.Get(3)).ReturnsAsync(category);

            // Act
            var result = await _service.Get(3);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Physics", result.CategoryName);
            _mockRepository.Verify(r => r.Get(3), Times.Once);
        }

        [Test]
        public void GetAll_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Astronomy" },
                new Category { CategoryID = 2, CategoryName = "Astrology" }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(categories);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public async Task Update_ValidCategory_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var category = new Category { CategoryID = 4, CategoryName = "Astrophysics" };

            // Act
            await _service.Update(category);

            // Assert
            _mockRepository.Verify(r => r.Update(category), Times.Once);
        }
    }
}
