using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminClientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminClientsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return View(clients);
        }

        public IActionResult Create()
        {
            return View(new Client());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var folder = Path.Combine(_env.WebRootPath, "images", "clients");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);

                model.LogoUrl = "/images/clients/" + fileName;
            }

            _context.Clients.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client model)
        {
            if (id != model.Id)
                return NotFound();

            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            client.Name = model.Name;
            client.WebsiteUrl = model.WebsiteUrl;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var folder = Path.Combine(_env.WebRootPath, "images", "clients");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);

                client.LogoUrl = "/images/clients/" + fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}