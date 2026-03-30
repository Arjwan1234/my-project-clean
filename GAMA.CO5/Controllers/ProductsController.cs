using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && category != "الكل")
            {
                query = query.Where(p => p.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                query = query.Where(p =>
                    p.Name.Contains(q) ||
                    p.Description.Contains(q) ||
                    p.Category.Contains(q));
            }

            var products = query.ToList();

            var featuredProducts = _context.Products
                .Where(p => p.IsFeatured)
                .ToList();

            var categories = new List<string> { "الكل" };
            categories.AddRange(_context.Products
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .ToList());

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