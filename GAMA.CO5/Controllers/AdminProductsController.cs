using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .OrderByDescending(p => p.IsFeatured)
                .ThenByDescending(p => p.Id)
                .ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (model.IsFeatured)
            {
                var featuredCount = await _context.Products.CountAsync(p => p.IsFeatured);
                if (featuredCount >= 3)
                {
                    ModelState.AddModelError("IsFeatured", "لا يمكن إضافة أكثر من 3 منتجات مميزة. قم بإلغاء تمييز منتج آخر أولاً.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                model.ImageUrl = await SaveImageAsync(model.ImageFile);
            }
            else
            {
                model.ImageUrl = "/images/products/placeholder.jpg";
            }

            _context.Products.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model)
        {
            if (model.IsFeatured)
            {
                var featuredCount = await _context.Products.CountAsync(p => p.IsFeatured && p.Id != model.Id);
                if (featuredCount >= 3)
                {
                    ModelState.AddModelError("IsFeatured", "لا يمكن جعل هذا المنتج مميزًا لأن الحد الأقصى هو 3 منتجات مميزة.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
                return NotFound();

            product.Name = model.Name;
            product.Description = model.Description;
            product.Category = model.Category;
            product.IsFeatured = model.IsFeatured;
            product.BadgeText = model.BadgeText;
            product.BadgeClass = model.BadgeClass;
            product.Features = model.Features;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(product.ImageUrl) &&
                    product.ImageUrl != "/images/products/placeholder.jpg")
                {
                    var oldImagePath = Path.Combine(
                        _environment.WebRootPath,
                        product.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
                    );

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                product.ImageUrl = await SaveImageAsync(model.ImageFile);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(product.ImageUrl) &&
                product.ImageUrl != "/images/products/placeholder.jpg")
            {
                var oldImagePath = Path.Combine(
                    _environment.WebRootPath,
                    product.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
                );

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/products/" + fileName;
        }
    }
}