using System.ComponentModel.DataAnnotations;

namespace GAMA.CO5.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string AboutMe { get; set; } = string.Empty;

        public string? JobTitle { get; set; }

        public string? CVFileName { get; set; }
    }
}