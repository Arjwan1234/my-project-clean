using System.ComponentModel.DataAnnotations;

namespace GAMA.CO5.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = "/images/products/placeholder.jpg";

        public string Category { get; set; } = "الكل";

        public bool IsFeatured { get; set; } = false;

        public string BadgeText { get; set; } = "منتج";

        public string BadgeClass { get; set; } = "badge-purple";

        public string Features { get; set; } = string.Empty;
    }
}