using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAMA.CO5.Models;
using GAMA.CO5.Data;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class CareersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CareersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new CareersViewModel
            {
                PageTitle = "الوظائف",
                Jobs = await _context.Jobs
                    .OrderByDescending(j => j.CreatedAt)
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(JobApplication model, IFormFile CVFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "تأكد من تعبئة جميع الحقول المطلوبة";
                return RedirectToAction("Index");
            }

            if (CVFile != null && CVFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "cv"
                );

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(CVFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CVFile.CopyToAsync(stream);
                }

                model.CVFileName = fileName;
            }

            _context.JobApplications.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "تم إرسال طلبك بنجاح";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("AdminJobApplications")]
        public async Task<IActionResult> AdminJobApplications()
        {
            var applications = await _context.JobApplications
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(applications);
        }
    }
}