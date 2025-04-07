using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GalacticUniversity.Controllers;
using GalacticUniversity.Core.CategoryService;
using GalacticUniversity.Core.CloudinaryService;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Core.LectureService;
using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.UserService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class CourseControllerTests
    {
        private Mock<ICourseService> _courseServiceMock;
        private Mock<ICategoryService> _categoryServiceMock;
        private Mock<ILectureService> _lectureServiceMock;
        private Mock<CloudinaryService> _cloudinaryServiceMock;
        private Mock<IUserCourseService> _userCourseServiceMock;
        private Mock<IUserService<User>> _userServiceMock;
        private Mock<UserManager<User>> _userManagerMock;
        private CourseController _courseController;
        private User _currentUser;

        [SetUp]
        public void Setup()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _lectureServiceMock = new Mock<ILectureService>();
            _cloudinaryServiceMock = new Mock<CloudinaryService>();
            _userCourseServiceMock = new Mock<IUserCourseService>();
            _userServiceMock = new Mock<IUserService<User>>();
            _userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

            _currentUser = new User { Id = "user1", UserName = "TestUser" };
            _userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(_currentUser);

            _courseController = new CourseController(
                _courseServiceMock.Object,
                _categoryServiceMock.Object,
                _lectureServiceMock.Object,
                _cloudinaryServiceMock.Object,
                _userCourseServiceMock.Object,
                _userServiceMock.Object,
                _userManagerMock.Object);

            var tempData = new Mock<ITempDataDictionary>();
            _courseController.TempData = tempData.Object;

            var httpContext = new DefaultHttpContext();
            _courseController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TearDown]
        public void TearDown()
        {
            _courseController.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithFilteredCourses()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course { CourseID = 1, CategoryID = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10) }
            }.AsQueryable();
            var categories = new List<Category> { new Category { CategoryID = 1, CategoryName = "Cat1" } }.AsQueryable();
            var userCourses = new List<UserCourses> { new UserCourses { UserID = "user1", CourseID = 2 } }.AsQueryable();

            _courseServiceMock.Setup(x => x.GetAll()).Returns(courses);
            _categoryServiceMock.Setup(x => x.GetAll()).Returns(categories);
            _userCourseServiceMock.Setup(x => x.GetAll()).Returns(userCourses);

            var filter = new CourseViewModel { CategoryID = 1 };

            // Act
            var result = await _courseController.Index(filter) as ViewResult;
            var model = result?.Model as CourseViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Courses.Count, Is.EqualTo(1));
            Assert.That(model.Courses[0].CourseID, Is.EqualTo(1));
            Assert.That(model.Categories.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task Add_Get_ReturnsViewWithCategoriesAndLectures()
        {
            // Arrange
            var categories = new List<Category> { new Category { CategoryID = 1, CategoryName = "Cat1" } }.AsQueryable();
            var lectures = new List<Lecture> { new Lecture { LectureID = 1, LectureName = "Lec1" } }.AsQueryable();
            _categoryServiceMock.Setup(x => x.GetAll()).Returns(categories);
            _lectureServiceMock.Setup(x => x.GetAll()).Returns(lectures);

            // Act
            var result = await _courseController.Add() as ViewResult;
            var model = result?.Model as CourseQueryViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Categories.Count, Is.EqualTo(1));
            Assert.That(model.Lectures.Count, Is.EqualTo(1));
            Assert.That(model.StartDate, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public async Task Add_Post_ValidCourse_AddsSuccessfully()
        {
            // Arrange
            var cvm = new CourseQueryViewModel
            {
                CourseName = "TestCourse",
                Description = "TestDesc",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                CategoryID = 1,
                SelectedLecturesID = 1,
                Image = new Mock<IFormFile>().Object
            };
            var lectures = new List<Lecture> { new Lecture { LectureID = 1, LectureName = "Lec1" } }.AsQueryable();
            _lectureServiceMock.Setup(x => x.GetAll()).Returns(lectures);
            _cloudinaryServiceMock.Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>())).ReturnsAsync("http://image.url");

            // Act
            var result = await _courseController.Add(cvm, null) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            _courseServiceMock.Verify(x => x.Add(It.Is<Course>(c =>
                c.CourseName == "TestCourse" &&
                c.ImageURL == "http://image.url" &&
                c.Lectures.Count == 1)), Times.Once);
            Assert.That(_courseController.TempData["success"], Is.EqualTo("Succesfully added course!"));
        }

        [Test]
        public async Task Edit_Get_ValidId_ReturnsViewWithCourse()
        {
            // Arrange
            var course = new Course
            {
                CourseID = 1,
                CourseName = "TestCourse",
                CategoryID = 1,
                ImageURL = "http://old.url"
            };
            var categories = new List<Category> { new Category { CategoryID = 1, CategoryName = "Cat1" } }.AsQueryable();
            _courseServiceMock.Setup(x => x.Get(1)).ReturnsAsync(course);
            _categoryServiceMock.Setup(x => x.GetAll()).Returns(categories);

            // Act
            var result = await _courseController.Edit(1) as ViewResult;
            var model = result?.Model as CourseQueryViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.ID, Is.EqualTo(1));
            Assert.That(model.CourseName, Is.EqualTo("TestCourse"));
            Assert.That(model.ImageURL, Is.EqualTo("http://old.url"));
            Assert.That(model.Categories.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Edit_Post_ValidCourse_UpdatesSuccessfully()
        {
            // Arrange
            var cvm = new CourseQueryViewModel
            {
                ID = 1,
                CourseName = "UpdatedCourse",
                Description = "UpdatedDesc",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                CategoryID = 1,
                Image = new Mock<IFormFile>().Object
            };
            var existingCourse = new Course { CourseID = 1, ImageURL = "http://old.url" };
            _courseServiceMock.Setup(x => x.Get(1)).ReturnsAsync(existingCourse);
            _cloudinaryServiceMock.Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>())).ReturnsAsync("http://new.url");

            // Act
            var result = await _courseController.Edit(cvm) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            _courseServiceMock.Verify(x => x.Update(It.Is<Course>(c =>
                c.CourseName == "UpdatedCourse" &&
                c.ImageURL == "http://new.url")), Times.Once);
            Assert.That(_courseController.TempData["success"], Is.EqualTo("Successfully edited course!"));
        }

        [Test]
        public async Task Delete_ValidId_DeletesSuccessfully()
        {
            // Arrange
            var course = new Course { CourseID = 1 };
            _courseServiceMock.Setup(x => x.Get(1)).ReturnsAsync(course);

            // Act
            var result = await _courseController.Delete(1) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            _courseServiceMock.Verify(x => x.Delete(course), Times.Once);
            Assert.That(_courseController.TempData["success"], Is.EqualTo("Succesfully deleted course!"));
        }

        [Test]
        public async Task Details_ValidId_ReturnsViewWithCourseDetails()
        {
            // Arrange
            var course = new Course
            {
                CourseID = 1,
                Lectures = new List<Lecture> { new Lecture { LectureResources = new List<LectureResource>() } },
                Category = new Category(),
                Comments = new List<Comment> { new Comment { User = new User() } }
            };
            var coursesQuery = new List<Course> { course }.AsQueryable();
            _courseServiceMock.Setup(x => x.GetAll()).Returns(coursesQuery);

            // Act
            var result = await _courseController.Details(1) as ViewResult;
            var model = result?.Model as Course;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CourseID, Is.EqualTo(1));
        }

        [Test]
        public async Task JoinCourse_ValidId_JoinsSuccessfully()
        {
            // Arrange
            _userManagerMock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("user1");

            // Act
            var result = await _courseController.JoinCourse(1) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            _userCourseServiceMock.Verify(x => x.JoinCourse("user1", 1), Times.Once);
            Assert.That(_courseController.TempData["success"], Is.EqualTo("Check your profile!"));
        }
    }
}