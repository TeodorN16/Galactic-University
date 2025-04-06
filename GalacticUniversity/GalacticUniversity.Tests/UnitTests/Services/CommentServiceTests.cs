using Moq;
using NUnit.Framework;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Core.CommentService;
using GalacticUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GalacticUniversity.Tests.UnitTests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private Mock<IRepository<Comment>> _mockRepository;
        private CommentService _commentService;

        [SetUp]
        public void Setup()
        {
            // Create the mock repository for Comment
            _mockRepository = new Mock<IRepository<Comment>>();

            // Pass the mock repository to the CommentService constructor
            _commentService = new CommentService(_mockRepository.Object);
        }

        [Test]
        public async Task Add_ShouldCallRepositoryAdd()
        {
            // Arrange
            var comment = new Comment
            {
                CommentID = 1,
                CommentText = "Great course content!",
                CommentDate = DateTime.Now,
                Rating = 5,
                UserID = "user123",
                CourseID = 101
            };

            _mockRepository.Setup(r => r.Add(It.IsAny<Comment>())).Returns(Task.CompletedTask);

            // Act
            await _commentService.Add(comment);

            // Assert
            _mockRepository.Verify(r => r.Add(It.Is<Comment>(c =>
                c.CommentID == 1 &&
                c.CommentText == "Great course content!" &&
                c.Rating == 5 &&
                c.UserID == "user123" &&
                c.CourseID == 101)), Times.Once);
        }

        [Test]
        public async Task Delete_ShouldCallRepositoryDelete()
        {
            // Arrange
            var comment = new Comment
            {
                CommentID = 2,
                CommentText = "Could use more examples",
                CommentDate = DateTime.Now.AddDays(-1),
                Rating = 3,
                UserID = "user456",
                CourseID = 102
            };

            _mockRepository.Setup(r => r.Delete(It.IsAny<Comment>())).Returns(Task.CompletedTask);

            // Act
            await _commentService.Delete(comment);

            // Assert
            _mockRepository.Verify(r => r.Delete(It.Is<Comment>(c =>
                c.CommentID == 2 &&
                c.CommentText == "Could use more examples" &&
                c.Rating == 3)), Times.Once);
        }

        [Test]
        public async Task Get_ShouldCallRepositoryGet()
        {
            // Arrange
            var user = new User
            {
                Id = "user789",
                UserName = "john_doe",
                Email = "john@example.com"
            };

            var course = new Course
            {
                CourseID = 103,
                CourseName = "Advanced Physics",
                Description = "Deep dive into physics concepts"
            };

            var comment = new Comment
            {
                CommentID = 3,
                CommentText = "Very informative lectures",
                CommentDate = DateTime.Now.AddDays(-2),
                Rating = 4,
                UserID = "user789",
                User = user,
                CourseID = 103,
                Course = course
            };

            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(comment);

            // Act
            var result = await _commentService.Get(3);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.CommentID);
            Assert.AreEqual("Very informative lectures", result.CommentText);
            Assert.AreEqual(4, result.Rating);
            Assert.IsNotNull(result.User);
            Assert.AreEqual("john_doe", result.User.UserName);
            Assert.IsNotNull(result.Course);
            Assert.AreEqual("Advanced Physics", result.Course.CourseName);
            _mockRepository.Verify(r => r.Get(3), Times.Once);
        }

        [Test]
        public async Task Get_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Comment)null);

            // Act
            var result = await _commentService.Get(999);

            // Assert
            Assert.IsNull(result);
            _mockRepository.Verify(r => r.Get(999), Times.Once);
        }

        [Test]
        public void GetAll_ShouldCallRepositoryGetAll()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment
                {
                    CommentID = 4,
                    CommentText = "Excellent course material",
                    CommentDate = DateTime.Now.AddDays(-5),
                    Rating = 5,
                    UserID = "user101",
                    CourseID = 201
                },
                new Comment
                {
                    CommentID = 5,
                    CommentText = "Good but needs more practice exercises",
                    CommentDate = DateTime.Now.AddDays(-3),
                    Rating = 4,
                    UserID = "user102",
                    CourseID = 202
                },
                new Comment
                {
                    CommentID = 6,
                    CommentText = "Very detailed explanations",
                    CommentDate = DateTime.Now.AddDays(-1),
                    Rating = 5,
                    UserID = "user103",
                    CourseID = 201
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(comments);

            // Act
            var result = _commentService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Excellent course material", result.First().CommentText);
            Assert.AreEqual("Very detailed explanations", result.Last().CommentText);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public async Task Update_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var comment = new Comment
            {
                CommentID = 7,
                CommentText = "Updated: Really enjoyed this course",
                CommentDate = DateTime.Now,
                Rating = 5,
                UserID = "user104",
                CourseID = 301
            };

            _mockRepository.Setup(r => r.Update(It.IsAny<Comment>())).Returns(Task.CompletedTask);

            // Act
            await _commentService.Update(comment);

            // Assert
            _mockRepository.Verify(r => r.Update(It.Is<Comment>(c =>
                c.CommentID == 7 &&
                c.CommentText == "Updated: Really enjoyed this course" &&
                c.Rating == 5)), Times.Once);
        }

        [Test]
        public void GetAll_FilterByCourseId_ShouldReturnFilteredResults()
        {
            // Arrange
            var targetCourseId = 401;

            var comments = new List<Comment>
            {
                new Comment
                {
                    CommentID = 8,
                    CommentText = "Comment for specific course 1",
                    CommentDate = DateTime.Now.AddDays(-5),
                    Rating = 4,
                    UserID = "user105",
                    CourseID = targetCourseId
                },
                new Comment
                {
                    CommentID = 9,
                    CommentText = "Comment for specific course 2",
                    CommentDate = DateTime.Now.AddDays(-2),
                    Rating = 5,
                    UserID = "user106",
                    CourseID = targetCourseId
                },
                new Comment
                {
                    CommentID = 10,
                    CommentText = "Comment for different course",
                    CommentDate = DateTime.Now.AddDays(-1),
                    Rating = 3,
                    UserID = "user107",
                    CourseID = 402
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(comments);

            // Act
            var result = _commentService.GetAll().Where(c => c.CourseID == targetCourseId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(targetCourseId, result.First().CourseID);
            Assert.AreEqual(targetCourseId, result.Last().CourseID);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public void GetAll_FilterByRating_ShouldReturnFilteredResults()
        {
            // Arrange
            var targetRating = 5;

            var comments = new List<Comment>
            {
                new Comment
                {
                    CommentID = 11,
                    CommentText = "Excellent course!",
                    CommentDate = DateTime.Now.AddDays(-10),
                    Rating = targetRating,
                    UserID = "user108",
                    CourseID = 501
                },
                new Comment
                {
                    CommentID = 12,
                    CommentText = "Good course",
                    CommentDate = DateTime.Now.AddDays(-8),
                    Rating = 4,
                    UserID = "user109",
                    CourseID = 502
                },
                new Comment
                {
                    CommentID = 13,
                    CommentText = "Perfect content and delivery",
                    CommentDate = DateTime.Now.AddDays(-5),
                    Rating = targetRating,
                    UserID = "user110",
                    CourseID = 503
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(comments);

            // Act
            var result = _commentService.GetAll().Where(c => c.Rating == targetRating);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(targetRating, result.First().Rating);
            Assert.AreEqual(targetRating, result.Last().Rating);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public void GetAll_OrderByDate_ShouldReturnOrderedResults()
        {
            // Arrange
            var date1 = DateTime.Now.AddDays(-15);
            var date2 = DateTime.Now.AddDays(-10);
            var date3 = DateTime.Now.AddDays(-5);

            var comments = new List<Comment>
            {
                new Comment
                {
                    CommentID = 14,
                    CommentText = "Oldest comment",
                    CommentDate = date1,
                    Rating = 4,
                    UserID = "user111",
                    CourseID = 601
                },
                new Comment
                {
                    CommentID = 15,
                    CommentText = "Middle comment",
                    CommentDate = date2,
                    Rating = 3,
                    UserID = "user112",
                    CourseID = 602
                },
                new Comment
                {
                    CommentID = 16,
                    CommentText = "Newest comment",
                    CommentDate = date3,
                    Rating = 5,
                    UserID = "user113",
                    CourseID = 603
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(comments);

            // Act - Get newest first
            var newestFirstResult = _commentService.GetAll().OrderByDescending(c => c.CommentDate);

            // Assert
            Assert.IsNotNull(newestFirstResult);
            Assert.AreEqual(3, newestFirstResult.Count());
            Assert.AreEqual(16, newestFirstResult.First().CommentID); // Newest should be first
            Assert.AreEqual(14, newestFirstResult.Last().CommentID); // Oldest should be last

            // Act - Get oldest first
            var oldestFirstResult = _commentService.GetAll().OrderBy(c => c.CommentDate);

            // Assert
            Assert.IsNotNull(oldestFirstResult);
            Assert.AreEqual(3, oldestFirstResult.Count());
            Assert.AreEqual(14, oldestFirstResult.First().CommentID); // Oldest should be first
            Assert.AreEqual(16, oldestFirstResult.Last().CommentID); // Newest should be last

            _mockRepository.Verify(r => r.GetAll(), Times.Exactly(2));
        }

        [Test]
        public void GetAll_GroupByRating_ShouldReturnGroupedResults()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment
                {
                    CommentID = 17,
                    CommentText = "Five star comment 1",
                    CommentDate = DateTime.Now.AddDays(-10),
                    Rating = 5,
                    UserID = "user114",
                    CourseID = 701
                },
                new Comment
                {
                    CommentID = 18,
                    CommentText = "Four star comment",
                    CommentDate = DateTime.Now.AddDays(-9),
                    Rating = 4,
                    UserID = "user115",
                    CourseID = 702
                },
                new Comment
                {
                    CommentID = 19,
                    CommentText = "Five star comment 2",
                    CommentDate = DateTime.Now.AddDays(-8),
                    Rating = 5,
                    UserID = "user116",
                    CourseID = 703
                },
                new Comment
                {
                    CommentID = 20,
                    CommentText = "Three star comment",
                    CommentDate = DateTime.Now.AddDays(-7),
                    Rating = 3,
                    UserID = "user117",
                    CourseID = 704
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(comments);

            // Act
            var result = _commentService.GetAll()
                .GroupBy(c => c.Rating)
                .Select(g => new { Rating = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Rating)
                .ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count); // Should have 3 rating groups (3, 4, 5)

            var fiveStarGroup = result.FirstOrDefault(g => g.Rating == 5);
            Assert.IsNotNull(fiveStarGroup);
            Assert.AreEqual(2, fiveStarGroup.Count); // Should have 2 five-star comments

            var fourStarGroup = result.FirstOrDefault(g => g.Rating == 4);
            Assert.IsNotNull(fourStarGroup);
            Assert.AreEqual(1, fourStarGroup.Count); // Should have 1 four-star comment

            var threeStarGroup = result.FirstOrDefault(g => g.Rating == 3);
            Assert.IsNotNull(threeStarGroup);
            Assert.AreEqual(1, threeStarGroup.Count); // Should have 1 three-star comment

            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }
    }
}