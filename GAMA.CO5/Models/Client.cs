using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAMA.CO5.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? WebsiteUrl { get; set; }

        public string? LogoUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}