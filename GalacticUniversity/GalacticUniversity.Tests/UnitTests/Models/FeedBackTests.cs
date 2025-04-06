using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.Models
{
    [TestFixture]
    public class FeedbackTests
    {
        [Test]
        public void Feedback_Should_Set_Properties_Correctly()
        {
            // Arrange
            var feedback = new Feedback
            {
                Id = 1,
                UserName = "JohnDoe",
                Email = "john@example.com",
                Text = "Great course!",
                UserID = "user123"
            };

            // Act & Assert
            Assert.AreEqual(1, feedback.Id);
            Assert.AreEqual("JohnDoe", feedback.UserName);
            Assert.AreEqual("john@example.com", feedback.Email);
            Assert.AreEqual("Great course!", feedback.Text);
            Assert.AreEqual("user123", feedback.UserID);
            Assert.That(feedback.CreatedAt, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(5)));
        }

        [Test]
        public void Feedback_Should_Have_Default_CreatedAt_Value()
        {
            // Act
            var feedback = new Feedback();

            // Assert
            Assert.That(feedback.CreatedAt, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(5)));
        }

        [Test]
        public void Feedback_Model_Should_Pass_Validation()
        {
            // Arrange
            var feedback = new Feedback
            {
                UserName = "Alice",
                Email = "alice@example.com",
                Text = "Helpful content!",
                UserID = "user456"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(feedback, null, null);
            var isValid = Validator.TryValidateObject(feedback, context, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }
    }
}
