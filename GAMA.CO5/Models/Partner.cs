using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GAMA.CO5.Models
{
    public class Partner
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string LogoUrl { get; set; } = "";

        public string Description { get; set; } = "";

        public string WebsiteUrl { get; set; } = "";

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}