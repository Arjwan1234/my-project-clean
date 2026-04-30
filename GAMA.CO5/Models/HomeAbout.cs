using System.ComponentModel.DataAnnotations;

namespace GAMA.CO5.Models
{
    public class HomeAbout
    {
        public int Id { get; set; }

        public string? BadgeText { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Stat1Number { get; set; }
        public string? Stat1Text { get; set; }

        public string? Stat2Number { get; set; }
        public string? Stat2Text { get; set; }

        public string? Stat3Number { get; set; }
        public string? Stat3Text { get; set; }
    }
}