using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GAMA.CO5.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        [Display(Name = "اسم المنتج")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "الوصف مطلوب")]
        [Display(Name = "الوصف")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "رابط الصورة")]
        public string ImageUrl { get; set; } = "/images/products/placeholder.jpg";

        [NotMapped]
        [Display(Name = "صورة المنتج")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "التصنيف")]
        public string Category { get; set; } = "الكل";

        [Display(Name = "مميز")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "نص البادج")]
        public string BadgeText { get; set; } = "منتج";

        [Display(Name = "كلاس البادج")]
        public string BadgeClass { get; set; } = "badge-purple";

        [Display(Name = "المميزات")]
        public string Features { get; set; } = string.Empty;

    }

}