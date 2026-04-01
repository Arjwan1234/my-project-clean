using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class PartnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var partners = _context.Partners.ToList();
            return View(partners);
        }

        public IActionResult Details(int id)
        {
            var partner = _context.Partners.FirstOrDefault(p => p.Id == id);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }
    }
}
