using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class AdminServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminServicesController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult Create(Service model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Services.Add(model);
            _context.SaveChanges();

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
        public IActionResult Edit(Service model)
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
            service.ImageUrl = model.ImageUrl;
            service.BadgeText = model.BadgeText;
            service.BadgeColor = model.BadgeColor;
            service.Features = model.Features;

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

            _context.Services.Remove(service);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}