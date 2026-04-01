using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            var partners = _context.Partners.ToList();
            return View(partners);
        }

        public IActionResult Create()
        {
            return View(new Partner());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Partner model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (imageFile != null && imageFile.Length > 0)
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
                    imageFile.CopyTo(stream);
                }

                model.LogoUrl = "/images/partners/" + fileName;
            }

            _context.Partners.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var partner = _context.Partners.FirstOrDefault(p => p.Id == id);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Partner model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var partner = _context.Partners.FirstOrDefault(p => p.Id == model.Id);

            if (partner == null)
            {
                return NotFound();
            }

            partner.Name = model.Name;
            partner.Description = model.Description;
            partner.WebsiteUrl = model.WebsiteUrl;

            if (imageFile != null && imageFile.Length > 0)
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
                    imageFile.CopyTo(stream);
                }

                partner.LogoUrl = "/images/partners/" + fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var partner = _context.Partners.FirstOrDefault(p => p.Id == id);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var partner = _context.Partners.FirstOrDefault(p => p.Id == id);

            if (partner == null)
            {
                return NotFound();
            }

            _context.Partners.Remove(partner);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}