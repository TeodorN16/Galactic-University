using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GalacticUniversity.Controllers;
using GalacticUniversity.Core.CommentService;
using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Models;
using GalacticUniversity.Models.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace GalacticUniversity.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class CommentControllerTests
    {
        private Mock<ICommentService> _commentServiceMock;
        private Mock<ICourseService> _courseServiceMock;
        private Mock<UserManager<User>> _userManagerMock;
        private CommentController _commentController;
        private User _currentUser;
        private Dictionary<string, object> _tempDataDictionary;

        [SetUp]
        public void Setup()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _courseServiceMock = new Mock<ICourseService>();
            _userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

            _currentUser = new User { Id = "user1", UserName = "TestUser" };
            _userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(_currentUser);

            _commentController = new CommentController(_commentServiceMock.Object, _userManagerMock.Object, _courseServiceMock.Object);

            // Use a real dictionary to back TempData
            _tempDataDictionary = new Dictionary<string, object>();
            var tempData = new Mock<ITempDataDictionary>();
            tempData.Setup(t => t[It.IsAny<string>()])
                .Returns<string>(key => _tempDataDictionary.ContainsKey(key) ? _tempDataDictionary[key] : null);
            tempData.SetupSet(t => t[It.IsAny<string>()] = It.IsAny<object>())
                .Callback<string, object>((key, value) => _tempDataDictionary[key] = value);
            _commentController.TempData = tempData.Object;

            var httpContext = new DefaultHttpContext();
            _commentController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TearDown]
        public void TearDown()
        {
            _commentController.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithAllComments()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { CommentID = 1, CommentText = "Great course!", Rating = 5, CourseID = 1 },
                new Comment { CommentID = 2, CommentText = "Needs improvement", Rating = 3, CourseID = 2 }
            }.AsQueryable();
            _commentServiceMock.Setup(x => x.GetAll()).Returns(comments);

            // Act
            var result = await _commentController.Index() as ViewResult;
            var model = result?.Model as IEnumerable<CommentViewModel>;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(2));
            Assert.That(model.First().ID, Is.EqualTo(1));
            Assert.That(model.First().CommentText, Is.EqualTo("Great course!"));
        }

        [Test]
        public async Task Add_Get_ReturnsViewWithCourses()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course { CourseID = 1, CourseName = "Course1" },
                new Course { CourseID = 2, CourseName = "Course2" }
            }.AsQueryable();
            _courseServiceMock.Setup(x => x.GetAll()).Returns(courses);

            // Act
            var result = await _commentController.Add() as ViewResult;
            var model = result?.Model as CommentViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Courses.Count, Is.EqualTo(2));
            Assert.That(model.Courses[0].Value, Is.EqualTo("1"));
            Assert.That(model.Courses[0].Text, Is.EqualTo("Course1"));
        }

        [Test]
        public async Task Add_Post_ValidComment_AddsSuccessfullyForUser()
        {
            // Arrange
            var commentViewModel = new CommentViewModel
            {
                CommentText = "Nice course!",
                Rating = 4,
                CourseID = 1
            };
            _commentController.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "User") }));

            // Act
            var result = await _commentController.Add(commentViewModel) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Learn"));
            Assert.That(result.ControllerName, Is.EqualTo("User"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(1));
            _commentServiceMock.Verify(x => x.Add(It.Is<Comment>(c =>
                c.CommentText == "Nice course!" &&
                c.Rating == 4 &&
                c.UserID == "user1" &&
                c.CourseID == 1)), Times.Once);
            Assert.That(_commentController.TempData["success"], Is.EqualTo("Comment added successfully!"));
        }

        [Test]
        public async Task Add_Post_InvalidComment_ReturnsViewWithError()
        {
            // Arrange
            var commentViewModel = new CommentViewModel
            {
                CommentText = "", // Invalid: empty comment
                CourseID = 0      // Invalid: no course selected
            };
            var courses = new List<Course> { new Course { CourseID = 1, CourseName = "Course1" } }.AsQueryable();
            _courseServiceMock.Setup(x => x.GetAll()).Returns(courses);

            // Act
            var result = await _commentController.Add(commentViewModel) as ViewResult;
            var model = result?.Model as CommentViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(_commentController.TempData["error"], Is.EqualTo("Please check your inputs and try again."));
            Assert.That(model.Courses.Count, Is.EqualTo(1));
            _commentServiceMock.Verify(x => x.Add(It.IsAny<Comment>()), Times.Never);
        }

        [Test]
        public async Task Edit_Get_ValidId_ReturnsViewWithComment()
        {
            // Arrange
            var comment = new Comment
            {
                CommentID = 1,
                CommentText = "Test comment",
                Rating = 5,
                CourseID = 1,
                Course = new Course { CourseName = "Course1" }
            };
            var comments = new List<Comment> { comment }.AsQueryable();
            _commentServiceMock.Setup(x => x.Get(1)).ReturnsAsync(comment);
            _commentServiceMock.Setup(x => x.GetAll()).Returns(comments);

            // Act
            var result = await _commentController.Edit(1) as ViewResult;
            var model = result?.Model as CommentViewModel;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.ID, Is.EqualTo(1));
            Assert.That(model.CommentText, Is.EqualTo("Test comment"));
            Assert.That(model.Courses.Count, Is.EqualTo(1));
            Assert.That(model.Courses[0].Text, Is.EqualTo("Course1"));
        }

        [Test]
        public async Task Edit_Get_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _commentServiceMock.Setup(x => x.Get(1)).ReturnsAsync((Comment)null);

            // Act
            var result = await _commentController.Edit(1) as IActionResult;

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_ValidComment_UpdatesSuccessfully()
        {
            // Arrange
            var commentViewModel = new CommentViewModel
            {
                ID = 1,
                CommentText = "Updated comment",
                Rating = 4,
                CourseID = 1
            };
            var existingComment = new Comment { CommentID = 1, CourseID = 1 };
            _commentServiceMock.Setup(x => x.Get(1)).ReturnsAsync(existingComment);

            // Act
            var result = await _commentController.Edit(commentViewModel) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(result.ControllerName, Is.EqualTo("Course"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(1));
            _commentServiceMock.Verify(x => x.Update(It.Is<Comment>(c =>
                c.CommentID == 1 &&
                c.CommentText == "Updated comment" &&
                c.Rating == 4)), Times.Once);
        }

        [Test]
        public async Task Delete_ValidId_DeletesSuccessfully()
        {
            // Arrange
            var comment = new Comment { CommentID = 1, CourseID = 2 };
            _commentServiceMock.Setup(x => x.Get(1)).ReturnsAsync(comment);
            _commentController.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Admin") }));

            // Act
            var result = await _commentController.Delete(1) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Comment"));
            _commentServiceMock.Verify(x => x.Delete(comment), Times.Once);
            Assert.That(_commentController.TempData["success"], Is.EqualTo("Succesfully deleted comment"));
        }

        [Test]
        public async Task GetCommentsForCourse_ReturnsCommentsForCourse()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { CommentID = 1, CourseID = 1, CommentText = "Comment1" },
                new Comment { CommentID = 2, CourseID = 2, CommentText = "Comment2" }
            }.AsQueryable();
            _commentServiceMock.Setup(x => x.GetAll()).Returns(comments);

            // Act
            var result = await _commentController.GetCommentsForCourse(1) as ViewResult;
            var model = result?.Model as IQueryable<Comment>;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(1));
            Assert.That(model.First().CommentID, Is.EqualTo(1));
        }
    }
}