using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string category = "الكل", string q = "")
        {
            category = string.IsNullOrWhiteSpace(category) ? "الكل" : category;
            q = q?.Trim() ?? "";

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(p =>
                    p.Name.Contains(q) ||
                    p.Description.Contains(q) ||
                    p.Category.Contains(q));
            }

            if (!string.IsNullOrWhiteSpace(category) && category != "الكل" && category != "مميزة")
            {
                query = query.Where(p => p.Category == category);
            }

            List<Product> products;

            if (category == "مميزة")
            {
                products = _context.Products
                    .Where(p => p.IsFeatured)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
            else
            {
                products = query
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }

            var featuredProducts = _context.Products
                .Where(p => p.IsFeatured)
                .OrderByDescending(p => p.Id)
                .Take(3)
                .ToList();

            var categories = new List<string> { "الكل", "مميزة" };

            var dbCategories = _context.Products
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .ToList();

            foreach (var c in dbCategories)
            {
                if (!categories.Contains(c))
                {
                    categories.Add(c);
                }
            }

            ViewBag.PageTitle = "منتجاتنا";
            ViewBag.SelectedCategory = category;
            ViewBag.Query = q;
            ViewBag.Categories = categories;
            ViewBag.FeaturedProducts = featuredProducts;

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}