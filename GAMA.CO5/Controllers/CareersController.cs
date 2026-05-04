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

        // ✅ عرض الوظائف للزوار
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

        // ✅ إرسال طلب توظيف
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(JobApplication model, IFormFile CVFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "تأكد من تعبئة جميع الحقول المطلوبة";
                return RedirectToAction("Index");
            }

            // رفع ملف CV
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
    }
}