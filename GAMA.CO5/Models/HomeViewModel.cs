namespace GAMA.CO5.Models
{
    public class HomeViewModel
    {
        public string CompanyName { get; set; } = "الشركة العربية الشاملة للتطبيقات المحدودة";
        public string Tagline { get; set; } = "شريكك الموثوق في التحول الرقمي";
        public List<Service> FeaturedServices { get; set; } = new();
    }
}
