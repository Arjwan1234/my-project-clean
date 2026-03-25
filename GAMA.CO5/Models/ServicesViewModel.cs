using System.Collections.Generic;

namespace GAMA.CO5.Models
{
    public class ServicesViewModel
    {
        public string PageTitle { get; set; } = "خدماتنا التقنية";
        public List<Service> Services { get; set; } = new();
    }

    public class Service
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Icon { get; set; } = "";

        public string ImageUrl { get; set; } = "";
        public string BadgeText { get; set; } = "";
        public string BadgeColor { get; set; } = "";

        public List<string> Features { get; set; } = new();
    }
}