using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAMA.CO5.Models
{
    public class HomeNews
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime NewsDate { get; set; } = DateTime.Now;

        public string? ImageUrl { get; set; }

        public string? LinkUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}