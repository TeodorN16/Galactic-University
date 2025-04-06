using System;
using System.Collections.Generic;
using GalacticUniversity.Models;
using NUnit.Framework;

namespace GalacticUniversity.Tests.Models
{
    [TestFixture]
    public class TarotModelsTests
    {
        [Test]
        public void TarotCard_Should_Set_Properties_Correctly()
        {
            // Arrange
            var card = new TarotCard
            {
                Id = 1,
                Name = "The Fool",
                ImageUrl = "http://example.com/fool.jpg",
                Description = "New beginnings, spontaneity, free spirit.",
                MeaningLove = "A fresh start in your love life.",
                MeaningCareer = "New job or venture.",
                MeaningGeneral = "Take a leap of faith.",
                IsReversed = false
            };

            // Assert
            Assert.AreEqual(1, card.Id);
            Assert.AreEqual("The Fool", card.Name);
            Assert.AreEqual("http://example.com/fool.jpg", card.ImageUrl);
            Assert.AreEqual("New beginnings, spontaneity, free spirit.", card.Description);
            Assert.AreEqual("A fresh start in your love life.", card.MeaningLove);
            Assert.AreEqual("New job or venture.", card.MeaningCareer);
            Assert.AreEqual("Take a leap of faith.", card.MeaningGeneral);
            Assert.IsFalse(card.IsReversed);
        }

        [Test]
        public void TarotReading_Should_Initialize_Empty_Cards_List()
        {
            // Arrange
            var reading = new TarotReading();

            // Assert
            Assert.IsNotNull(reading.Cards);
            Assert.IsEmpty(reading.Cards);
        }

        [Test]
        public void TarotReading_Should_Store_Cards_And_Interpretation()
        {
            // Arrange
            var card1 = new TarotCard { Name = "The Sun" };
            var card2 = new TarotCard { Name = "The Moon" };
            var reading = new TarotReading
            {
                ReadingType = "General",
                Interpretation = "Positive outlook with hidden emotions.",
                Cards = new List<TarotCard> { card1, card2 }
            };

            // Assert
            Assert.AreEqual("General", reading.ReadingType);
            Assert.AreEqual("Positive outlook with hidden emotions.", reading.Interpretation);
            Assert.AreEqual(2, reading.Cards.Count);
            Assert.AreEqual("The Sun", reading.Cards[0].Name);
            Assert.AreEqual("The Moon", reading.Cards[1].Name);
        }

        [Test]
        public void TarotViewModel_Should_Hold_Reading_And_HasReading_Flag()
        {
            // Arrange
            var reading = new TarotReading
            {
                ReadingType = "Love",
                Interpretation = "New romance ahead.",
                Cards = new List<TarotCard> { new TarotCard { Name = "Lovers" } }
            };

            var viewModel = new TarotViewModel
            {
                CurrentReading = reading,
                HasReading = true
            };

            // Assert
            Assert.IsTrue(viewModel.HasReading);
            Assert.IsNotNull(viewModel.CurrentReading);
            Assert.AreEqual("Love", viewModel.CurrentReading.ReadingType);
            Assert.AreEqual("New romance ahead.", viewModel.CurrentReading.Interpretation);
            Assert.AreEqual("Lovers", viewModel.CurrentReading.Cards[0].Name);
        }
    }
}
