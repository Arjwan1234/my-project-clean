namespace GAMA.CO5.Models
{
    public class CareersViewModel
    {
        public List<Job> Jobs { get; set; } = new();
        public string PageTitle { get; set; } = "انضم لفريقنا";
    }

    public class Job
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
