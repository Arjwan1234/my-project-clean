using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class AdminPartnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPartnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔥 الحل هنا
        public async Task<IActionResult> Index()
        {
            var partners = await _context.Partners.ToListAsync();

            // تحديد المسار يدويًا لمنع أي لخبطة
            return View("~/Views/AdminPartners/Index.cshtml", partners);
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
                return View(model);

            _context.Partners.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null) return NotFound();

            return View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Partner model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null) return NotFound();

            return View(partner);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partners.FindAsync(id);

            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}