using System.ComponentModel.DataAnnotations;

namespace GAMA.CO5.Models
{
    public class CareersViewModel
    {
        public List<Job> Jobs { get; set; } = new();
        public string PageTitle { get; set; } = "الوظائف";
    }

    public class Job
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}