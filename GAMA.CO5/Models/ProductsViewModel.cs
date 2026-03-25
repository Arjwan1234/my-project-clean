namespace GAMA.CO5.Models
{
    public class ProductsViewModel
    {
        public string PageTitle { get; set; } = "المنتجات";
        public List<Product> FeaturedProducts { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<string> Categories { get; set; } = new();
    }

    public class Product
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "/images/products/placeholder.jpg";
        public string Category { get; set; } = "الكل";
        public bool IsFeatured { get; set; } = false;
        public string BadgeText { get; set; } = "منتج";
        public string BadgeClass { get; set; } = "badge-purple";
        public List<string> Features { get; set; } = new();
    }
}