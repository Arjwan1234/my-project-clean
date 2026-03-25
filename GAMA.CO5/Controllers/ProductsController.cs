using Microsoft.AspNetCore.Mvc;
using GAMA.CO5.Models;
using System.Collections.Generic;
using System.Linq;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(string category = "الكل", string q = "")
        {
            var allProducts = GetAllProducts();

            var categories = new List<string> { "الكل" };
            categories.AddRange(allProducts
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .ToList());

            var filteredProducts = allProducts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && category != "الكل")
            {
                filteredProducts = filteredProducts.Where(p => p.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                filteredProducts = filteredProducts.Where(p =>
                    p.Name.Contains(q) ||
                    p.Description.Contains(q) ||
                    p.Category.Contains(q));
            }

            var featuredIds = new[] { "qobool", "vesa", "mudar" };

            var featuredProducts = allProducts
                .Where(p => featuredIds.Contains(p.Id))
                .OrderBy(p => System.Array.IndexOf(featuredIds, p.Id))
                .ToList();

            var model = new ProductsViewModel
            {
                PageTitle = "منتجاتنا",
                Products = filteredProducts.ToList(),
                FeaturedProducts = featuredProducts,
                Categories = categories
            };

            ViewBag.SelectedCategory = category;
            ViewBag.Query = q;

            return View(model);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var product = GetAllProducts().FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        private List<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "mudar",
                    Name = "رحلة ذكية متكاملة",
                    Description = "حل ذكي متكامل لإدارة الحج والعمرة وتحسين تجربة الضيوف.",
                    Category = "الحج والعمرة",
                    ImageUrl = "/images/products/mudar.jpg",
                    IsFeatured = true,
                    BadgeText = "مدار",
                    BadgeClass = "badge-orange",
                    Features = new List<string>
                    {
                        "إدارة متكاملة لرحلات الحج والعمرة",
                        "تحسين تجربة الضيوف والخدمات",
                        "تقارير ومتابعة لحظية"
                    }
                },

                new Product
                {
                    Id = "vesa",
                    Name = "حلول ذكية ومتطورة",
                    Description = "تطبيق VESA يقدم حلول متكاملة وذكية باستخدام أحدث التقنيات.",
                    Category = "أعمال",
                    ImageUrl = "/images/products/vesa.jpg",
                    IsFeatured = true,
                    BadgeText = "VESA",
                    BadgeClass = "badge-blue",
                    Features = new List<string>
                    {
                        "واجهات حديثة وسهلة الاستخدام",
                        "تكامل مع الأنظمة المختلفة",
                        "أداء عالي ومرونة تشغيل"
                    }
                },

                new Product
                {
                    Id = "qobool",
                    Name = "إدارة الحج والعمرة بكفاءة عالية",
                    Description = "منصة قبول باج هي نظام إدارة شامل لرحلات الحج والعمرة.",
                    Category = "الحج والعمرة",
                    ImageUrl = "/images/products/qobool.jpg",
                    IsFeatured = true,
                    BadgeText = "قبول باج",
                    BadgeClass = "badge-pink",
                    Features = new List<string>
                    {
                        "إدارة الحجوزات والطلبات",
                        "تنظيم الرحلات والعمليات",
                        "لوحات متابعة وتقارير تشغيلية"
                    }
                },

                new Product
                {
                    Id = "erp",
                    Name = "GAMA ERP",
                    Description = "نظام متكامل لإدارة موارد مؤسستك بكفاءة.",
                    Category = "مالي",
                    ImageUrl = "/images/products/logo-gama.jpg",
                    IsFeatured = false,
                    BadgeText = "منتج",
                    BadgeClass = "badge-green",
                    Features = new List<string>
                    {
                        "إدارة الموارد والعمليات",
                        "تقارير مالية وتشغيلية",
                        "تنظيم أعمال المؤسسة بكفاءة"
                    }
                }
            };
        }
    }
}