using Moq;
using NUnit.Framework;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Core.FeedbackService;
using GalacticUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GalacticUniversity.Tests.UnitTests
{
    [TestFixture]
    public class FeedbackServiceTests
    {
        private Mock<IRepository<Feedback>> _mockRepository;
        private FeedbackService _feedbackService;

        [SetUp]
        public void Setup()
        {
            // Create the mock repository for Feedback
            _mockRepository = new Mock<IRepository<Feedback>>();

            // Pass the mock repository to the FeedbackService constructor
            _feedbackService = new FeedbackService(_mockRepository.Object);
        }

        [Test]
        public async Task Add_ShouldCallRepositoryAdd()
        {
            // Arrange
            var feedback = new Feedback
            {
                Id = 1,
                UserName = "John Doe",
                Email = "john@example.com",
                Text = "Great courses!",
                UserID = "user123",
                CreatedAt = DateTime.Now
            };

            _mockRepository.Setup(r => r.Add(It.IsAny<Feedback>())).Returns(Task.CompletedTask);

            // Act
            await _feedbackService.Add(feedback);

            // Assert
            _mockRepository.Verify(r => r.Add(It.Is<Feedback>(f =>
                f.UserName == "John Doe" &&
                f.Email == "john@example.com" &&
                f.Text == "Great courses!" &&
                f.UserID == "user123")), Times.Once);
        }

        [Test]
        public async Task Delete_ShouldCallRepositoryDelete()
        {
            // Arrange
            var feedback = new Feedback
            {
                Id = 2,
                UserName = "Jane Smith",
                Email = "jane@example.com",
                Text = "Needs improvement",
                UserID = "user456",
                CreatedAt = DateTime.Now
            };

            _mockRepository.Setup(r => r.Delete(It.IsAny<Feedback>())).Returns(Task.CompletedTask);

            // Act
            await _feedbackService.Delete(feedback);

            // Assert
            _mockRepository.Verify(r => r.Delete(It.Is<Feedback>(f =>
                f.Id == 2 &&
                f.UserName == "Jane Smith")), Times.Once);
        }

        [Test]
        public async Task Get_ShouldCallRepositoryGet()
        {
            // Arrange
            var feedback = new Feedback
            {
                Id = 3,
                UserName = "Bob Johnson",
                Email = "bob@example.com",
                Text = "Excellent teaching!",
                UserID = "user789",
                CreatedAt = DateTime.Now
            };

            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(feedback);

            // Act
            var result = await _feedbackService.Get(3);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Id);
            Assert.AreEqual("Bob Johnson", result.UserName);
            Assert.AreEqual("bob@example.com", result.Email);
            Assert.AreEqual("Excellent teaching!", result.Text);
            Assert.AreEqual("user789", result.UserID);
            _mockRepository.Verify(r => r.Get(3), Times.Once);
        }

        [Test]
        public async Task Get_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Feedback)null);

            // Act
            var result = await _feedbackService.Get(999);

            // Assert
            Assert.IsNull(result);
            _mockRepository.Verify(r => r.Get(999), Times.Once);
        }

        [Test]
        public void GetAll_ShouldCallRepositoryGetAll()
        {
            // Arrange
            var feedbacks = new List<Feedback>
            {
                new Feedback
                {
                    Id = 4,
                    UserName = "Carol Davis",
                    Email = "carol@example.com",
                    Text = "Very informative",
                    UserID = "user101",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new Feedback
                {
                    Id = 5,
                    UserName = "Dave Wilson",
                    Email = "dave@example.com",
                    Text = "Could use more examples",
                    UserID = "user102",
                    CreatedAt = DateTime.Now.AddDays(-2)
                },
                new Feedback
                {
                    Id = 6,
                    UserName = "Eve Brown",
                    Email = "eve@example.com",
                    Text = "Engaging content",
                    UserID = "user103",
                    CreatedAt = DateTime.Now.AddDays(-1)
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(feedbacks);

            // Act
            var result = _feedbackService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Carol Davis", result.First().UserName);
            Assert.AreEqual("Eve Brown", result.Last().UserName);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public async Task Update_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var feedback = new Feedback
            {
                Id = 7,
                UserName = "Frank Miller",
                Email = "frank@example.com",
                Text = "Updated feedback content",
                UserID = "user104",
                CreatedAt = DateTime.Now
            };

            _mockRepository.Setup(r => r.Update(It.IsAny<Feedback>())).Returns(Task.CompletedTask);

            // Act
            await _feedbackService.Update(feedback);

            // Assert
            _mockRepository.Verify(r => r.Update(It.Is<Feedback>(f =>
                f.Id == 7 &&
                f.Text == "Updated feedback content")), Times.Once);
        }

        [Test]
        public void GetAll_FilterByUserId_ShouldReturnFilteredResults()
        {
            // Arrange
            var targetUserId = "user105";

            var feedbacks = new List<Feedback>
            {
                new Feedback
                {
                    Id = 8,
                    UserName = "Grace Lee",
                    Email = "grace@example.com",
                    Text = "Feedback from specific user 1",
                    UserID = targetUserId,
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new Feedback
                {
                    Id = 9,
                    UserName = "Grace Lee",
                    Email = "grace@example.com",
                    Text = "Feedback from specific user 2",
                    UserID = targetUserId,
                    CreatedAt = DateTime.Now.AddDays(-3)
                },
                new Feedback
                {
                    Id = 10,
                    UserName = "Harry Potter",
                    Email = "harry@example.com",
                    Text = "Feedback from different user",
                    UserID = "user106",
                    CreatedAt = DateTime.Now.AddDays(-1)
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(feedbacks);

            // Act
            var result = _feedbackService.GetAll().Where(f => f.UserID == targetUserId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(targetUserId, result.First().UserID);
            Assert.AreEqual(targetUserId, result.Last().UserID);
            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public void GetAll_OrderByCreatedAt_ShouldReturnOrderedResults()
        {
            // Arrange
            var date1 = DateTime.Now.AddDays(-10);
            var date2 = DateTime.Now.AddDays(-5);
            var date3 = DateTime.Now.AddDays(-1);

            var feedbacks = new List<Feedback>
            {
                new Feedback
                {
                    Id = 11,
                    UserName = "Ian Smith",
                    Email = "ian@example.com",
                    Text = "Oldest feedback",
                    UserID = "user107",
                    CreatedAt = date1
                },
                new Feedback
                {
                    Id = 12,
                    UserName = "Jack Taylor",
                    Email = "jack@example.com",
                    Text = "Middle feedback",
                    UserID = "user108",
                    CreatedAt = date2
                },
                new Feedback
                {
                    Id = 13,
                    UserName = "Kate Wilson",
                    Email = "kate@example.com",
                    Text = "Newest feedback",
                    UserID = "user109",
                    CreatedAt = date3
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetAll()).Returns(feedbacks);

            // Act - Get newest first
            var newestFirstResult = _feedbackService.GetAll().OrderByDescending(f => f.CreatedAt);

            // Assert
            Assert.IsNotNull(newestFirstResult);
            Assert.AreEqual(3, newestFirstResult.Count());
            Assert.AreEqual(13, newestFirstResult.First().Id); // Newest should be first
            Assert.AreEqual(11, newestFirstResult.Last().Id); // Oldest should be last

            // Act - Get oldest first
            var oldestFirstResult = _feedbackService.GetAll().OrderBy(f => f.CreatedAt);

            // Assert
            Assert.IsNotNull(oldestFirstResult);
            Assert.AreEqual(3, oldestFirstResult.Count());
            Assert.AreEqual(11, oldestFirstResult.First().Id); // Oldest should be first
            Assert.AreEqual(13, oldestFirstResult.Last().Id); // Newest should be last

            _mockRepository.Verify(r => r.GetAll(), Times.Exactly(2));
        }

        [Test]
        public void Feedback_CreatedAtDefaultValue_ShouldBeCurrentTime()
        {
            // Arrange
            var before = DateTime.Now.AddSeconds(-1);

            // Act
            var feedback = new Feedback
            {
                Id = 14,
                UserName = "Larry Johnson",
                Email = "larry@example.com",
                Text = "Testing default timestamp",
                UserID = "user110"
                // Note: Not setting CreatedAt to test default value
            };
            var after = DateTime.Now.AddSeconds(1);

            // Assert
            Assert.IsTrue(feedback.CreatedAt >= before && feedback.CreatedAt <= after,
                "CreatedAt should default to current time");
        }
    }
}