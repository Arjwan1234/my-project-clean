namespace GAMA.CO5.Models
{
    public class HomeViewModel
    {
        public string PageTitle { get; set; } = "الرئيسية";

        public HeroSection Hero { get; set; } = new();
        public PromoSection Promo { get; set; } = new();

        public List<ServiceItem> Services { get; set; } = new();
        public List<PartnerItem> Partners { get; set; } = new();
        public List<ClientItem> Clients { get; set; } = new();
    }

    public class HeroSection
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string VideoPath { get; set; } = string.Empty;
        public string ButtonController { get; set; } = string.Empty;
        public string ButtonAction { get; set; } = string.Empty;
    }

    public class PromoSection
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<StatItem> Stats { get; set; } = new();
        public List<FeatureItem> Features { get; set; } = new();
    }

    public class StatItem
    {
        public string Icon { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }

    public class FeatureItem
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
    }

    public class ServiceItem
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LinkText { get; set; } = "اعرف المزيد";
        public string Controller { get; set; } = "Services";
        public string Action { get; set; } = "Index";
    }

    public class PartnerItem
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }

    public class ClientItem
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
    }
}