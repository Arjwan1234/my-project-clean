using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class AdminServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminServicesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        public IActionResult Create()
        {
            return View(new Service());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service model)
        {
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
                model.ImageUrl = "/images/services/placeholder.jpg";
            }

            _context.Services.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Service model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = _context.Services.FirstOrDefault(s => s.Id == model.Id);

            if (service == null)
            {
                return NotFound();
            }

            service.Title = model.Title;
            service.Description = model.Description;
            service.Icon = model.Icon;
            service.BadgeText = model.BadgeText;
            service.BadgeColor = model.BadgeColor;
            service.Features = model.Features;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                service.ImageUrl = await SaveImageAsync(model.ImageFile);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(service.ImageUrl) &&
                service.ImageUrl != "/images/services/placeholder.jpg")
            {
                var oldImagePath = Path.Combine(
                    _environment.WebRootPath,
                    service.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
                );

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _context.Services.Remove(service);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "services");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/services/" + fileName;
        }
    }
}