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
        public async Task<IActionResult> Create(Partner model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                model.LogoUrl = await SaveImageAsync(imageFile);
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
        public async Task<IActionResult> Edit(int id, Partner model, IFormFile? imageFile)
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

            if (imageFile != null && imageFile.Length > 0)
            {
                DeleteImage(partner.LogoUrl);
                partner.LogoUrl = await SaveImageAsync(imageFile);
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
            string folderPath = Path.Combine(_environment.WebRootPath, "images", "partners");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/partners/" + fileName;
        }

        private void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            string fullPath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}