using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Models
{
    public class TarotCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string MeaningLove { get; set; }
        public string MeaningCareer { get; set; }
        public string MeaningGeneral { get; set; }
        public bool IsReversed { get; set; }
    }
    public class TarotReading
    {
        public List<TarotCard> Cards { get; set; }
        public string ReadingType { get; set; } // "Love", "Career", or "General"
        public string Interpretation { get; set; }

        public TarotReading()
        {
            Cards = new List<TarotCard>();
        }
    }

    public class TarotViewModel
    {
        public TarotReading CurrentReading { get; set; }
        public bool HasReading { get; set; }
    }
}
