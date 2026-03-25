namespace GAMA.CO5.Models
{
    public class PartnersViewModel
    {
        public List<Partner> Partners { get; set; } = new();
        public string PageTitle { get; set; } = "شركاؤنا";
    }

    public class Partner
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
    }
}
