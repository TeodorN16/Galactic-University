using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.TarotCardService
{
    public class TarotService
    {
        private static Random random = new Random();
        private static List<TarotCard> allCards = new List<TarotCard>();

        static TarotService()
        {
            InitializeDeck();
        }

        private static void InitializeDeck()
        {
            // Major Arcana
            allCards.Add(new TarotCard
            {
                Id = 0,
                Name = "The Fool",
                ImageUrl = "/img/RWS_Tarot_00_Fool.jpg",
                MeaningGeneral = "New beginnings, freedom, innocence, spontaneity, a free spirit",
                MeaningLove = "New relationship possibilities, taking a chance on love",
                MeaningCareer = "New job opportunities, taking risks in your career"
            });

            allCards.Add(new TarotCard
            {
                Id = 1,
                Name = "The Magician",
                ImageUrl = "/img/RWS_Tarot_01_Magician.jpg",
                MeaningGeneral = "Manifestation, resourcefulness, power, inspired action",
                MeaningLove = "Creating the relationship you want through intention",
                MeaningCareer = "Using your skills effectively, manifesting success"
            });

            allCards.Add(new TarotCard
            {
                Id = 2,
                Name = "The High Priestess",
                ImageUrl = "/img/RWS_Tarot_02_High_Priestess.jpg",
                MeaningGeneral = "Intuition, sacred knowledge, divine feminine, the subconscious mind",
                MeaningLove = "Listen to your intuition about relationship matters",
                MeaningCareer = "Trust your inner knowledge about your work direction"
            });

            // Add more cards as needed - this is just a sample
            // In a real implementation, you'd add all 78 cards

            allCards.Add(new TarotCard
            {
                Id = 3,
                Name = "The Empress",
                ImageUrl = "/img/RWS_Tarot_03_Empress.jpg",
                MeaningGeneral = "Femininity, beauty, nature, nurturing, abundance",
                MeaningLove = "Nurturing relationships, fertility, sensuality",
                MeaningCareer = "Creative expression, growth in your career"
            });

            allCards.Add(new TarotCard
            {
                Id = 4,
                Name = "The Emperor",
                ImageUrl = "/img/RWS_Tarot_04_Emperor.jpg",
                MeaningGeneral = "Authority, establishment, structure, a father figure",
                MeaningLove = "Stable relationship, commitment, traditional values",
                MeaningCareer = "Leadership role, structure, organization"
            });

            allCards.Add(new TarotCard
            {
                Id = 5,
                Name = "The Hierophant",
                ImageUrl = "/img/RWS_Tarot_05_Hierophant.jpg",
                MeaningGeneral = "Spiritual wisdom, religious beliefs, conformity, tradition",
                MeaningLove = "Traditional relationships, shared values and beliefs",
                MeaningCareer = "Learning from a mentor, traditional career path"
            });

            allCards.Add(new TarotCard
            {
                Id = 6,
                Name = "The Lovers",
                ImageUrl = "/img/RWS_Tarot_06_Lovers.jpg",
                MeaningGeneral = "Love, harmony, relationships, values alignment, choices",
                MeaningLove = "Deep connection, alignment of values, important choice in love",
                MeaningCareer = "Partnership, aligning work with your values"
            });

            allCards.Add(new TarotCard
            {
                Id = 7,
                Name = "The Chariot",
                ImageUrl = "/img/RWS_Tarot_07_Chariot.jpg",
                MeaningGeneral = "Control, willpower, success, action, determination",
                MeaningLove = "Taking control of your love life, moving forward",
                MeaningCareer = "Career advancement through determination"
            });

            allCards.Add(new TarotCard
            {
                Id = 8,
                Name = "Strength",
                ImageUrl = "/img/RWS_Tarot_08_Strength.jpg",
                MeaningGeneral = "Courage, patience, control, compassion, self-control",
                MeaningLove = "Inner strength in relationships, patience with partner",
                MeaningCareer = "Resilience at work, gentle but firm leadership"
            });

            allCards.Add(new TarotCard
            {
                Id = 9,
                Name = "The Hermit",
                ImageUrl = "/img/RWS_Tarot_09_Hermit.jpg",
                MeaningGeneral = "Soul searching, introspection, being alone, inner guidance",
                MeaningLove = "Time for reflection on relationships, solitude",
                MeaningCareer = "Independent work, seeking deeper meaning in career"
            });

            allCards.Add(new TarotCard
            {
                Id = 10,
                Name = "Wheel of Fortune",
                ImageUrl = "/img/RWS_Tarot_10_Wheel_of_Fortune.jpg",
                MeaningGeneral = "Good luck, karma, life cycles, destiny, a turning point",
                MeaningLove = "Destiny in relationships, turning point in love life",
                MeaningCareer = "Lucky break, positive change in fortune"
            });
            
            // Adding Justice (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 11,
                Name = "Justice",
                ImageUrl = "/img/RWS_Tarot_11_Justice.jpg",
                MeaningGeneral = "Fairness, truth, cause and effect, law, balance",
                MeaningLove = "Fair treatment in relationships, balance of give and take",
                MeaningCareer = "Fair outcomes, legal matters, ethical decisions"
            });

            // Adding Hanged Man (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 12,
                Name = "The Hanged Man",
                ImageUrl = "/img/RWS_Tarot_12_Hanged_Man.jpg",
                MeaningGeneral = "Surrender, letting go, new perspectives, sacrifice",
                MeaningLove = "Seeing relationship from a new angle, patience",
                MeaningCareer = "Taking time to reflect, sacrifice for long-term gain"
            });

            // Adding Death (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 13,
                Name = "Death",
                ImageUrl = "/img/RWS_Tarot_13_Death.jpg",
                MeaningGeneral = "Endings, change, transformation, transition",
                MeaningLove = "End of unhealthy patterns, transformation in relationships",
                MeaningCareer = "Career change, ending old work patterns"
            });

            // Adding Temperance (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 14,
                Name = "Temperance",
                ImageUrl = "/img/RWS_Tarot_14_Temperance.jpg",
                MeaningGeneral = "Balance, moderation, patience, purpose",
                MeaningLove = "Balanced approach to love, healing relationships",
                MeaningCareer = "Finding middle ground, patience in work matters"
            });

            // Adding The Devil (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 15,
                Name = "The Devil",
                ImageUrl = "/img/RWS_Tarot_15_Devil.jpg",
                MeaningGeneral = "Bondage, addiction, materialism, ignorance",
                MeaningLove = "Unhealthy attachments, dependencies in relationships",
                MeaningCareer = "Feeling trapped in work, unhealthy work environment"
            });

            // Adding The Tower (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 16,
                Name = "The Tower",
                ImageUrl = "/img/RWS_Tarot_16_Tower.jpg",
                MeaningGeneral = "Sudden change, upheaval, chaos, revelation, awakening",
                MeaningLove = "Sudden breakups, revelations in relationships",
                MeaningCareer = "Unexpected job loss, sudden career changes"
            });

            // Adding The Star (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 17,
                Name = "The Star",
                ImageUrl = "/img/RWS_Tarot_17_Star.jpg",
                MeaningGeneral = "Hope, faith, renewal, spirituality, inspiration",
                MeaningLove = "Renewed faith in love, healing after heartbreak",
                MeaningCareer = "Inspiration, finding your calling"
            });

            // Adding The Moon (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 18,
                Name = "The Moon",
                ImageUrl = "/img/RWS_Tarot_18_Moon.jpg",
                MeaningGeneral = "Illusion, fear, anxiety, confusion, subconscious",
                MeaningLove = "Uncertainties in relationships, hidden emotions",
                MeaningCareer = "Workplace confusion, unclear path forward"
            });

            // Adding The Sun (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 19,
                Name = "The Sun",
                ImageUrl = "/img/RWS_Tarot_19_Sun.jpg",
                MeaningGeneral = "Positivity, success, radiance, joy, enlightenment",
                MeaningLove = "Happy relationships, clarity in love matters",
                MeaningCareer = "Success, recognition, achievement"
            });

            // Adding Judgement (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 20,
                Name = "Judgement",
                ImageUrl = "/img/RWS_Tarot_20_Judgement.jpg",
                MeaningGeneral = "Rebirth, inner calling, absolution, renewal",
                MeaningLove = "Reconciliation, forgiveness in relationships",
                MeaningCareer = "Career calling, answering your true purpose"
            });

            // Adding The World (Major Arcana)
            allCards.Add(new TarotCard
            {
                Id = 21,
                Name = "The World",
                ImageUrl = "/img/RWS_Tarot_21_World.jpg",
                MeaningGeneral = "Completion, fulfillment, sense of belonging, wholeness",
                MeaningLove = "Fulfilling relationship, completion of relationship cycle",
                MeaningCareer = "Career fulfillment, completion of major projects"
            });

            // Cups (Minor Arcana)
            allCards.Add(new TarotCard
            {
                Id = 22,
                Name = "Ace of Cups",
                ImageUrl = "/img/Cups01.jpg",
                MeaningGeneral = "New feelings, intuition, intimacy, love, compassion",
                MeaningLove = "New emotional beginnings, potential for love",
                MeaningCareer = "Emotionally rewarding work opportunities"
            });

            allCards.Add(new TarotCard
            {
                Id = 23,
                Name = "Two of Cups",
                ImageUrl = "/img/Cups02.jpg",
                MeaningGeneral = "Unity, partnership, mutual respect, harmony",
                MeaningLove = "Balanced relationship, mutual attraction",
                MeaningCareer = "Beneficial partnership, collaboration"
            });

            allCards.Add(new TarotCard
            {
                Id = 24,
                Name = "Three of Cups",
                ImageUrl = "/img/Cups03.jpg",
                MeaningGeneral = "Celebration, friendship, creativity, community",
                MeaningLove = "Joyful relationships, social connections",
                MeaningCareer = "Team success, creative collaboration"
            });

            allCards.Add(new TarotCard
            {
                Id = 25,
                Name = "Four of Cups",
                ImageUrl = "/img/Cups04.jpg",
                MeaningGeneral = "Contemplation, apathy, reevaluation, discontent",
                MeaningLove = "Boredom in relationships, missing opportunities",
                MeaningCareer = "Work dissatisfaction, overlooking opportunities"
            });

            allCards.Add(new TarotCard
            {
                Id = 26,
                Name = "Five of Cups",
                ImageUrl = "/img/Cups05.jpg",
                MeaningGeneral = "Loss, regret, disappointment, focus on the negative",
                MeaningLove = "Relationship disappointment, focusing on what went wrong",
                MeaningCareer = "Career setbacks, focusing on failures"
            });

            allCards.Add(new TarotCard
            {
                Id = 27,
                Name = "Six of Cups",
                ImageUrl = "/img/Cups06.jpg",
                MeaningGeneral = "Nostalgia, childhood memories, innocence, joy",
                MeaningLove = "Rekindling relationships, innocent love",
                MeaningCareer = "Returning to past work, finding joy in work"
            });

            allCards.Add(new TarotCard
            {
                Id = 28,
                Name = "Seven of Cups",
                ImageUrl = "/img/Cups07.jpg",
                MeaningGeneral = "Choices, fantasy, illusion, wishful thinking",
                MeaningLove = "Many relationship options, idealistic views of love",
                MeaningCareer = "Multiple career paths, unrealistic work expectations"
            });

            allCards.Add(new TarotCard
            {
                Id = 29,
                Name = "Eight of Cups",
                ImageUrl = "/img/Cups08.jpg",
                MeaningGeneral = "Walking away, disillusionment, leaving behind",
                MeaningLove = "Moving on from relationships, seeking something deeper",
                MeaningCareer = "Leaving a job, seeking more fulfilling work"
            });

            allCards.Add(new TarotCard
            {
                Id = 30,
                Name = "Nine of Cups",
                ImageUrl = "/img/Cups09.jpg",
                MeaningGeneral = "Satisfaction, emotional fulfillment, wishes granted",
                MeaningLove = "Emotional contentment, relationship satisfaction",
                MeaningCareer = "Work satisfaction, achieving career goals"
            });

            allCards.Add(new TarotCard
            {
                Id = 31,
                Name = "Ten of Cups",
                ImageUrl = "/img/Cups10.jpg",
                MeaningGeneral = "Divine love, harmony, alignment, family bliss",
                MeaningLove = "Harmonious relationships, family happiness",
                MeaningCareer = "Workplace harmony, work-life balance"
            });

            allCards.Add(new TarotCard
            {
                Id = 32,
                Name = "Page of Cups",
                ImageUrl = "/img/Cups11.jpg",
                MeaningGeneral = "Creative beginnings, intuitive messages, curiosity",
                MeaningLove = "New emotional insights, romantic messages",
                MeaningCareer = "Creative opportunities, learning emotional skills"
            });

            allCards.Add(new TarotCard
            {
                Id = 33,
                Name = "Knight of Cups",
                ImageUrl = "/img/Cups12.jpg",
                MeaningGeneral = "Romantic, imaginative, idealist, refined",
                MeaningLove = "Romantic gestures, pursuing love interests",
                MeaningCareer = "Creative pursuits, following your passions"
            });

            allCards.Add(new TarotCard
            {
                Id = 34,
                Name = "Queen of Cups",
                ImageUrl = "/img/Cups13.jpg",
                MeaningGeneral = "Compassionate, caring, emotionally stable, intuitive",
                MeaningLove = "Nurturing love, emotional connection",
                MeaningCareer = "Emotionally intelligent leadership, caring professions"
            });

            allCards.Add(new TarotCard
            {
                Id = 35,
                Name = "King of Cups",
                ImageUrl = "/img/Cups14.jpg",
                MeaningGeneral = "Emotionally balanced, diplomatic, calm, wise",
                MeaningLove = "Mature emotional support, balanced approach to love",
                MeaningCareer = "Emotionally mature leadership, counseling roles"
            });

            // Pentacles (Minor Arcana)
            allCards.Add(new TarotCard
            {
                Id = 36,
                Name = "Ace of Pentacles",
                ImageUrl = "/img/Pents01.jpg",
                MeaningGeneral = "New financial or career opportunity, prosperity, abundance",
                MeaningLove = "Stable foundation for relationship, security",
                MeaningCareer = "New job offers, financial opportunities"
            });

            allCards.Add(new TarotCard
            {
                Id = 37,
                Name = "Two of Pentacles",
                ImageUrl = "/img/Pents02.jpg",
                MeaningGeneral = "Balance, adaptation, prioritization, juggling resources",
                MeaningLove = "Balancing relationship with other priorities",
                MeaningCareer = "Work-life balance, managing multiple projects"
            });

            allCards.Add(new TarotCard
            {
                Id = 38,
                Name = "Three of Pentacles",
                ImageUrl = "/img/Pents03.jpg",
                MeaningGeneral = "Teamwork, collaboration, skill, quality",
                MeaningLove = "Building relationship through cooperation",
                MeaningCareer = "Recognition for skills, teamwork success"
            });

            allCards.Add(new TarotCard
            {
                Id = 39,
                Name = "Four of Pentacles",
                ImageUrl = "/img/Pents04.jpg",
                MeaningGeneral = "Security, control, saving money, conservatism",
                MeaningLove = "Possessiveness in relationships, fear of vulnerability",
                MeaningCareer = "Financial security, holding onto position"
            });

            allCards.Add(new TarotCard
            {
                Id = 40,
                Name = "Five of Pentacles",
                ImageUrl = "/img/Pents05.jpg",
                MeaningGeneral = "Financial loss, poverty, insecurity, worry",
                MeaningLove = "Feeling excluded, hardship in relationships",
                MeaningCareer = "Job loss, financial hardship, career uncertainty"
            });

            allCards.Add(new TarotCard
            {
                Id = 41,
                Name = "Six of Pentacles",
                ImageUrl = "/img/Pents06.jpg",
                MeaningGeneral = "Generosity, charity, giving/receiving, shared wealth",
                MeaningLove = "Balanced give and take in relationships",
                MeaningCareer = "Receiving recognition, fair compensation"
            });

            allCards.Add(new TarotCard
            {
                Id = 42,
                Name = "Seven of Pentacles",
                ImageUrl = "/img/Pents07.jpg",
                MeaningGeneral = "Long-term view, sustainable results, patience, investment",
                MeaningLove = "Evaluating relationship progress, patience",
                MeaningCareer = "Assessing career progress, long-term investments"
            });

            allCards.Add(new TarotCard
            {
                Id = 43,
                Name = "Eight of Pentacles",
                ImageUrl = "/img/Pents08.jpg",
                MeaningGeneral = "Diligence, skill development, attention to detail, craftsmanship",
                MeaningLove = "Working on relationship skills, dedication",
                MeaningCareer = "Apprenticeship, skill building, focus on work"
            });

            allCards.Add(new TarotCard
            {
                Id = 44,
                Name = "Nine of Pentacles",
                ImageUrl = "/img/Pents09.jpg",
                MeaningGeneral = "Luxury, self-sufficiency, financial independence",
                MeaningLove = "Independence within relationship, self-worth",
                MeaningCareer = "Material success, enjoying fruits of labor"
            });

            allCards.Add(new TarotCard
            {
                Id = 45,
                Name = "Ten of Pentacles",
                ImageUrl = "/img/Pents10.jpg",
                MeaningGeneral = "Wealth, family, establishment, inheritance, tradition",
                MeaningLove = "Long-term relationship security, family harmony",
                MeaningCareer = "Long-term career success, financial security"
            });

            allCards.Add(new TarotCard
            {
                Id = 46,
                Name = "Page of Pentacles",
                ImageUrl = "/img/Pents11.jpg",
                MeaningGeneral = "New opportunity, studiousness, learning, manifestation",
                MeaningLove = "Practical approach to relationships, learning",
                MeaningCareer = "Study, training, new job opportunities"
            });

            allCards.Add(new TarotCard
            {
                Id = 47,
                Name = "Knight of Pentacles",
                ImageUrl = "/img/Pents12.jpg",
                MeaningGeneral = "Hard work, responsibility, practicality, conservatism",
                MeaningLove = "Slow and steady approach to relationships",
                MeaningCareer = "Methodical work ethic, reliability"
            });

            allCards.Add(new TarotCard
            {
                Id = 48,
                Name = "Queen of Pentacles",
                ImageUrl = "/img/Pents13.jpg",
                MeaningGeneral = "Nurturing, practical, abundant, down-to-earth",
                MeaningLove = "Practical nurturing in relationships, stability",
                MeaningCareer = "Practical leadership, creating comfortable work environment"
            });

            allCards.Add(new TarotCard
            {
                Id = 49,
                Name = "King of Pentacles",
                ImageUrl = "/img/Pents14.jpg",
                MeaningGeneral = "Wealth, business acumen, security, discipline, abundance",
                MeaningLove = "Providing stability and security in relationships",
                MeaningCareer = "Business leadership, financial success"
            });

            // Swords (Minor Arcana)
            allCards.Add(new TarotCard
            {
                Id = 50,
                Name = "Ace of Swords",
                ImageUrl = "/img/Swords01.jpg",
                MeaningGeneral = "Breakthrough, clarity, mental force, truth",
                MeaningLove = "Clarity in relationships, honest communication",
                MeaningCareer = "Mental breakthroughs, new ideas"
            });

            allCards.Add(new TarotCard
            {
                Id = 51,
                Name = "Two of Swords",
                ImageUrl = "/img/Swords02.jpg",
                MeaningGeneral = "Difficult choices, indecision, stalemate, denial",
                MeaningLove = "Relationship at crossroads, avoiding decision",
                MeaningCareer = "Career indecision, weighing options"
            });

            allCards.Add(new TarotCard
            {
                Id = 52,
                Name = "Three of Swords",
                ImageUrl = "/img/Swords03.jpg",
                MeaningGeneral = "Heartbreak, sorrow, grief, rejection",
                MeaningLove = "Heartbreak, painful truths in relationships",
                MeaningCareer = "Workplace disappointment, project failure"
            });

            allCards.Add(new TarotCard
            {
                Id = 53,
                Name = "Four of Swords",
                ImageUrl = "/img/Swords04.jpg",
                MeaningGeneral = "Rest, restoration, contemplation, recuperation",
                MeaningLove = "Taking a break from relationships, healing",
                MeaningCareer = "Recovery from burnout, mental rest"
            });

            allCards.Add(new TarotCard
            {
                Id = 54,
                Name = "Five of Swords",
                ImageUrl = "/img/Swords05.jpg",
                MeaningGeneral = "Conflict, tension, defeat, win at all costs",
                MeaningLove = "Arguments, hurtful words, relationship conflict",
                MeaningCareer = "Workplace conflicts, unethical competition"
            });

            allCards.Add(new TarotCard
            {
                Id = 55,
                Name = "Six of Swords",
                ImageUrl = "/img/Swords06.jpg",
                MeaningGeneral = "Transition, letting go, moving on, mental journey",
                MeaningLove = "Moving on from difficult relationships",
                MeaningCareer = "Career transition, leaving difficulties behind"
            });

            allCards.Add(new TarotCard
            {
                Id = 56,
                Name = "Seven of Swords",
                ImageUrl = "/img/Swords07.jpg",
                MeaningGeneral = "Deception, strategy, stealth, mental agility",
                MeaningLove = "Dishonesty in relationships, hidden agendas",
                MeaningCareer = "Workplace deception, strategic thinking"
            });

            allCards.Add(new TarotCard
            {
                Id = 57,
                Name = "Eight of Swords",
                ImageUrl = "/img/Swords08.jpg",
                MeaningGeneral = "Restriction, limitation, self-imposed obstacles",
                MeaningLove = "Feeling trapped in relationships, limiting beliefs",
                MeaningCareer = "Feeling restricted at work, mental blocks"
            });

            allCards.Add(new TarotCard
            {
                Id = 58,
                Name = "Nine of Swords",
                ImageUrl = "/img/Swords09.jpg",
                MeaningGeneral = "Anxiety, worry, fear, despair, nightmares",
                MeaningLove = "Relationship anxiety, worry about partner",
                MeaningCareer = "Work-related stress, anxiety about performance"
            });

            allCards.Add(new TarotCard
            {
                Id = 59,
                Name = "Ten of Swords",
                ImageUrl = "/img/Swords10.jpg",
                MeaningGeneral = "Endings, defeat, crisis, betrayal, rock bottom",
                MeaningLove = "Painful relationship ending, betrayal",
                MeaningCareer = "Career rock bottom, project failure"
            });

            allCards.Add(new TarotCard
            {
                Id = 60,
                Name = "Page of Swords",
                ImageUrl = "/img/Swords11.jpg",
                MeaningGeneral = "Curiosity, restlessness, mental energy, new ideas",
                MeaningLove = "Overthinking relationships, curious communication",
                MeaningCareer = "New intellectual challenges, research"
            });

            allCards.Add(new TarotCard
            {
                Id = 61,
                Name = "Knight of Swords",
                ImageUrl = "/img/Swords12.jpg",
                MeaningGeneral = "Action, impulsiveness, defending beliefs, intellectual",
                MeaningLove = "Rushing into relationships, direct communication",
                MeaningCareer = "Fast-paced work, ambitious pursuits"
            });

            allCards.Add(new TarotCard
            {
                Id = 62,
                Name = "Queen of Swords",
                ImageUrl = "/img/Swords13.jpg",
                MeaningGeneral = "Independent, unbiased judgment, clear boundaries, direct",
                MeaningLove = "Independence in relationships, clear boundaries",
                MeaningCareer = "Analytical thinking, fair leadership"
            });

            allCards.Add(new TarotCard
            {
                Id = 63,
                Name = "King of Swords",
                ImageUrl = "/img/Swords14.jpg",
                MeaningGeneral = "Intellectual power, authority, truth, analytical",
                MeaningLove = "Logical approach to relationships, clear thinking",
                MeaningCareer = "Intellectual leadership, authority figure"
            });

            // Wands (Minor Arcana) - I notice you didn't specifically list these, but I'm adding them for completeness
            allCards.Add(new TarotCard
            {
                Id = 64,
                Name = "Ace of Wands",
                ImageUrl = "/img/Wands01.jpg",
                MeaningGeneral = "Creation, willpower, inspiration, desire",
                MeaningLove = "Spark of new passion, creative relationship energy",
                MeaningCareer = "New inspiration, creative project beginnings"
            });

            allCards.Add(new TarotCard
            {
                Id = 65,
                Name = "Two of Wands",
                ImageUrl = "/img/Wands02.jpg",
                MeaningGeneral = "Planning, making decisions, leaving comfort zone",
                MeaningLove = "Relationship choices, planning future together",
                MeaningCareer = "Career planning, assessing opportunities"
            });

            allCards.Add(new TarotCard
            {
                Id = 66,
                Name = "Three of Wands",
                ImageUrl = "/img/Wands03.jpg",
                MeaningGeneral = "Progress, expansion, foresight, overseas opportunities",
                MeaningLove = "Relationship growth, looking to the future",
                MeaningCareer = "Business expansion, long-term vision"
            });

            allCards.Add(new TarotCard
            {
                Id = 67,
                Name = "Four of Wands",
                ImageUrl = "/img/Wands04.jpg",
                MeaningGeneral = "Celebration, harmony, marriage, home, community",
                MeaningLove = "Relationship milestone, harmony, celebration",
                MeaningCareer = "Work celebration, team harmony"
            });

            allCards.Add(new TarotCard
            {
                Id = 68,
                Name = "Five of Wands",
                ImageUrl = "/img/Wands05.jpg",
                MeaningGeneral = "Conflict, disagreements, competition, tension",
                MeaningLove = "Relationship conflicts, competing interests",
                MeaningCareer = "Workplace competition, creative differences"
            });

            allCards.Add(new TarotCard
            {
                Id = 69,
                Name = "Six of Wands",
                ImageUrl = "/img/Wands06.jpg",
                MeaningGeneral = "Victory, success, public recognition, progress",
                MeaningLove = "Relationship success, mutual admiration",
                MeaningCareer = "Professional recognition, achievement"
            });

            allCards.Add(new TarotCard
            {
                Id = 70,
                Name = "Seven of Wands",
                ImageUrl = "/img/Wands07.jpg",
                MeaningGeneral = "Challenge, competition, perseverance, defense",
                MeaningLove = "Defending relationship, standing your ground",
                MeaningCareer = "Workplace challenges, defending position"
            });

            allCards.Add(new TarotCard
            {
                Id = 71,
                Name = "Eight of Wands",
                ImageUrl = "/img/Wands08.jpg",
                MeaningGeneral = "Speed, action, air travel, movement, quick decisions",
                MeaningLove = "Rapid relationship development, quick communication",
                MeaningCareer = "Fast-moving projects, swift developments"
            });

            allCards.Add(new TarotCard
            {
                Id = 72,
                Name = "Nine of Wands",
                ImageUrl = "/img/Wands09.jpg",
                MeaningGeneral = "Resilience, courage, persistence, test of faith",
                MeaningLove = "Perseverance in relationships, last hurdle",
                MeaningCareer = "Workplace resilience, final push to success"
            });

            allCards.Add(new TarotCard
            {
                Id = 73,
                Name = "Ten of Wands",
                ImageUrl = "/img/Wands10.jpg",
                MeaningGeneral = "Burden, responsibility, hard work, accomplishment",
                MeaningLove = "Relationship burdens, taking on too much",
                MeaningCareer = "Work overload, carrying too many responsibilities"
            });

            allCards.Add(new TarotCard
            {
                Id = 74,
                Name = "Page of Wands",
                ImageUrl = "/img/Wands11.jpg",
                MeaningGeneral = "Exploration, excitement, freedom, adventure",
                MeaningLove = "Exciting new relationship prospects, flirtation",
                MeaningCareer = "New creative ideas, enthusiastic beginnings"
            });

            allCards.Add(new TarotCard
            {
                Id = 75,
                Name = "Knight of Wands",
                ImageUrl = "/img/Wands12.jpg",
                MeaningGeneral = "Energy, passion, adventure, impulsiveness",
                MeaningLove = "Passionate romance, exciting relationship",
                MeaningCareer = "Taking action, pursuing passions at work"
            });

            allCards.Add(new TarotCard
            {
                Id = 76,
                Name = "Queen of Wands",
                ImageUrl = "/img/Wands13.jpg",
                MeaningGeneral = "Courage, determination, joy, vibrancy, leadership",
                MeaningLove = "Passionate partner, confident relationship approach",
                MeaningCareer = "Confident leadership, inspiring others"
            });
            // Complete meaning for the King of Wands

            allCards.Add(new TarotCard
            {
                Id = 77,
                Name = "King of Wands",
                ImageUrl = "/img/Wands14.jpg",
                MeaningGeneral = "Leadership, vision, honor, entrepreneur, inspiration",
                MeaningLove = "Passionate leadership in relationships, protectiveness, bringing excitement and inspiration to love life, charismatic partner who encourages growth and adventure",
                MeaningCareer = "Visionary leadership, entrepreneurial spirit, creative authority, inspiring others through example, taking charge of your career path, bold business decisions, natural ability to motivate teams"
            });


            // Add more for a complete deck
        }

        public TarotReading GetReading(string readingType)
        {
            int numberOfCards = readingType == "General" ? 5 : 3;

            // Create a new reading
            var reading = new TarotReading
            {
                ReadingType = readingType
            };

            // Shuffle the deck
            var shuffledDeck = allCards.OrderBy(c => Guid.NewGuid()).ToList();

            // Draw the cards
            for (int i = 0; i < numberOfCards; i++)
            {
                var drawnCard = shuffledDeck[i];

                // Randomly determine if the card is reversed
                drawnCard.IsReversed = random.Next(2) == 0;

                reading.Cards.Add(drawnCard);
            }

            // Generate an interpretation based on the drawn cards
            reading.Interpretation = GenerateInterpretation(reading);

            return reading;
        }

        private string GenerateInterpretation(TarotReading reading)
        {
            string interpretation = $"Your {reading.ReadingType} reading reveals: ";

            // Combine meanings based on reading type
            foreach (var card in reading.Cards)
            {
                string meaning = reading.ReadingType switch
                {
                    "Love" => card.MeaningLove,
                    "Career" => card.MeaningCareer,
                    _ => card.MeaningGeneral
                };

                // Modify meaning if card is reversed
                if (card.IsReversed)
                {
                    interpretation += $"\n\nThe reversed {card.Name} suggests challenges with {meaning.ToLower()}";
                }
                else
                {
                    interpretation += $"\n\nThe {card.Name} indicates {meaning.ToLower()}";
                }
            }

            // Add a conclusion
            interpretation += $"\n\nThis {reading.ReadingType.ToLower()} reading suggests you should focus on ";

            switch (reading.ReadingType)
            {
                case "Love":
                    interpretation += "opening your heart to new possibilities while addressing any emotional blocks.";
                    break;
                case "Career":
                    interpretation += "aligning your work with your true purpose and recognizing opportunities for growth.";
                    break;
                default:
                    interpretation += "finding balance in all aspects of your life and embracing the journey ahead.";
                    break;
            }

            return interpretation;
        }
    }
}
