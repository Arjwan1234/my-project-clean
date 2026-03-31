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

        public async Task<IActionResult> Index()
        {
            var model = new CareersViewModel
            {
                PageTitle = "الوظائف",
                Jobs = await _context.Jobs
                    .Where(j => j.IsActive)
                    .OrderByDescending(j => j.CreatedAt)
                    .ToListAsync()
            };

            return View(model);
        }
    }
}