using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAMA.CO5.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        public string Icon { get; set; } = "";

        public string ImageUrl { get; set; } = "";

        public string BadgeText { get; set; } = "";

        public string BadgeColor { get; set; } = "";

        public string Features { get; set; } = "";

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}