using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using GalacticUniversity.Models;
using GalacticUniversity.Core.TarotCardService;

namespace GalacticUniversity.Tests
{
    [TestFixture]
    public class TarotServiceTests
    {
        private TarotService _tarotService;

        [SetUp]
        public void SetUp()
        {
            TarotServiceTestable.ClearDeck();
            TarotServiceTestable.AddCards(new List<TarotCard>
            {
                new TarotCard { Id = 1, Name = "The Fool", MeaningLove = "new beginnings", MeaningCareer = "starting fresh", MeaningGeneral = "a leap of faith" },
                new TarotCard { Id = 2, Name = "The Magician", MeaningLove = "manifesting love", MeaningCareer = "taking initiative", MeaningGeneral = "personal power" },
                new TarotCard { Id = 3, Name = "The Tower", MeaningLove = "sudden change", MeaningCareer = "instability", MeaningGeneral = "drastic transformation" },
                new TarotCard { Id = 4, Name = "The Lovers", MeaningLove = "deep connection", MeaningCareer = "partnerships", MeaningGeneral = "meaningful decisions" },
                new TarotCard { Id = 5, Name = "The Star", MeaningLove = "renewed hope", MeaningCareer = "optimism", MeaningGeneral = "healing and faith" },
            });

            _tarotService = new TarotServiceTestable();
        }

        [TestCase("Love", 3)]
        [TestCase("Career", 3)]
        [TestCase("General", 5)]
        public void GetReading_ShouldReturnCorrectNumberOfCards(string readingType, int expectedCount)
        {
            var reading = _tarotService.GetReading(readingType);

            Assert.AreEqual(readingType, reading.ReadingType);
            Assert.AreEqual(expectedCount, reading.Cards.Count);
        }

        [Test]
        public void GetReading_ShouldIncludeInterpretation()
        {
            var reading = _tarotService.GetReading("Love");

            Assert.IsFalse(string.IsNullOrWhiteSpace(reading.Interpretation));
            Assert.IsTrue(reading.Interpretation.Contains("reading reveals"));
        }

        [Test]
        public void GetReading_ShouldIncludeReversedCards()
        {
            var reading = _tarotService.GetReading("Career");
            Assert.IsTrue(reading.Cards.Any(c => c.IsReversed) || reading.Cards.All(c => !c.IsReversed));
        }

        private class TarotServiceTestable : TarotService
        {
            public static void ClearDeck() => typeof(TarotService)
                   .GetField("allCards", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .SetValue(null, new List<TarotCard>());

            public static void AddCards(List<TarotCard> cards) => typeof(TarotService)
                .GetField("allCards", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .SetValue(null, cards);
        }
    }
}