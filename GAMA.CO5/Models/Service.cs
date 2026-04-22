using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GAMA.CO5.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "الوصف مطلوب")]
        public string Description { get; set; } = string.Empty;

        public string? Icon { get; set; }

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public string? BadgeText { get; set; }

        public string? BadgeColor { get; set; }

        public string? Features { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public IFormFile? VideoFile { get; set; }
    }
}