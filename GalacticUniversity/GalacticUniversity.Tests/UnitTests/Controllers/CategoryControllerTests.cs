using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalacticUniversity.Controllers;
using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private Mock<ICategoryService> _categoryServiceMock;
        private CategoryController _categoryController;
        private Dictionary<string, object> _tempDataDictionary;

        [SetUp]
        public void Setup()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);

            // Use a real dictionary to back TempData
            _tempDataDictionary = new Dictionary<string, object>();
            var tempData = new Mock<ITempDataDictionary>();
            tempData.Setup(t => t[It.IsAny<string>()])
                .Returns<string>(key => _tempDataDictionary.ContainsKey(key) ? _tempDataDictionary[key] : null);
            tempData.SetupSet(t => t[It.IsAny<string>()] = It.IsAny<object>())
                .Callback<string, object>((key, value) => _tempDataDictionary[key] = value);
            _categoryController.TempData = tempData.Object;

            var httpContext = new DefaultHttpContext();
            _categoryController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TearDown]
        public void TearDown()
        {
            _categoryController.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Category1" },
                new Category { CategoryID = 2, CategoryName = "Category2" }
            }.AsQueryable();
            _categoryServiceMock.Setup(x => x.GetAll()).Returns(categories);

            // Act
            var result = await _categoryController.Index() as ViewResult;
            var model = result?.Model as List<CategoryQueryViewModel>;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(2));
            Assert.That(model[0].ID, Is.EqualTo(1));
            Assert.That(model[0].Name, Is.EqualTo("Category1"));
            Assert.That(model[1].ID, Is.EqualTo(2));
            Assert.That(model[1].Name, Is.EqualTo("Category2"));
        }

        [Test]
        public async Task Add_Get_ReturnsViewWithEmptyModel()
        {
            // Act
            var result = await _categoryController.Add() as ViewResult;
            var model = result?.Model as CategoryViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.ID, Is.EqualTo(0)); // Assuming default int value
            Assert.That(model.Name, Is.Null);
        }

        [Test]
        public async Task Add_Post_ValidCategory_AddsSuccessfully()
        {
            // Arrange
            var categoryViewModel = new CategoryViewModel
            {
                Name = "NewCategory"
            };

            // Act
            var result = await _categoryController.Add(categoryViewModel) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            _categoryServiceMock.Verify(x => x.Add(It.Is<Category>(c => c.CategoryName == "NewCategory")), Times.Once);
            Assert.That(_categoryController.TempData["success"], Is.EqualTo("Successfully added a category"));
        }

        [Test]
        public async Task Edit_Get_ValidId_ReturnsViewWithCategory()
        {
            // Arrange
            var category = new Category { CategoryID = 1, CategoryName = "TestCategory" };
            _categoryServiceMock.Setup(x => x.Get(1)).ReturnsAsync(category);

            // Act
            var result = await _categoryController.Edit(1) as ViewResult;
            var model = result?.Model as CategoryViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.ID, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("TestCategory"));
        }

        [Test]
        public async Task Edit_Post_ValidCategory_UpdatesSuccessfully()
        {
            // Arrange
            var categoryViewModel = new CategoryViewModel
            {
                ID = 1,
                Name = "UpdatedCategory"
            };

            // Act
            var result = await _categoryController.Edit(categoryViewModel) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            _categoryServiceMock.Verify(x => x.Update(It.Is<Category>(c =>
                c.CategoryID == 1 && c.CategoryName == "UpdatedCategory")), Times.Once);
            Assert.That(_categoryController.TempData["success"], Is.EqualTo("Successfully edited category"));
        }

        [Test]
        public async Task Delete_ValidId_DeletesSuccessfully()
        {
            // Arrange
            var category = new Category { CategoryID = 1, CategoryName = "TestCategory" };
            _categoryServiceMock.Setup(x => x.Get(1)).ReturnsAsync(category);

            // Act
            var result = await _categoryController.Delete(1) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            _categoryServiceMock.Verify(x => x.Delete(category), Times.Once);
            Assert.That(_categoryController.TempData["success"], Is.EqualTo("Successfully deleted category"));
        }
    }
}