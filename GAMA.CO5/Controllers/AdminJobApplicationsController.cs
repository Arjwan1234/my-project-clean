using GAMA.CO5.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class AdminJobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminJobApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applications = await _context.JobApplications
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(applications);
        }
    }
}