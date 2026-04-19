using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class AdminPartnersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminPartnersController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var partners = await _context.Partners.ToListAsync();
            return View(partners);
        }

        public IActionResult Create()
        {
            return View(new Partner());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Partner model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                model.LogoUrl = await SaveImageAsync(model.ImageFile);
            }
            else
            {
                model.LogoUrl = "/images/partners/placeholder.png";
            }

            _context.Partners.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تمت إضافة الشريك بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _context.Partners.FirstOrDefaultAsync(p => p.Id == id);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Partner model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var partner = await _context.Partners.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (partner == null)
            {
                return NotFound();
            }

            partner.Name = model.Name;
            partner.Description = model.Description;
            partner.WebsiteUrl = model.WebsiteUrl;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                DeleteImage(partner.LogoUrl);
                partner.LogoUrl = await SaveImageAsync(model.ImageFile);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم تعديل بيانات الشريك بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _context.Partners.FirstOrDefaultAsync(p => p.Id == id);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partners.FirstOrDefaultAsync(p => p.Id == id);

            if (partner == null)
            {
                return NotFound();
            }

            DeleteImage(partner.LogoUrl);

            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم حذف الشريك بنجاح";
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var folderPath = Path.Combine(_environment.WebRootPath, "images", "partners");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/partners/" + fileName;
        }

        private void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || imagePath == "/images/partners/placeholder.png")
                return;

            var fullPath = Path.Combine(
                _environment.WebRootPath,
                imagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
            );

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}