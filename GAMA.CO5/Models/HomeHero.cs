using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAMA.CO5.Models
{
    public class HomeHero
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Subtitle { get; set; }
        public string? BadgeText { get; set; }
        public string? VideoUrl { get; set; }

        [NotMapped]
        public IFormFile? VideoFile { get; set; }
    }
}