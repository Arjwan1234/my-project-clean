using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactMessage());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(ContactMessage model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            _context.ContactMessages.Add(model);
            _context.SaveChanges();

            ViewBag.Success = true;

            return View("Index", new ContactMessage());
        }

        [HttpGet]
        [Route("AdminContactMessages")]
        public IActionResult AdminContactMessages()
        {
            var messages = _context.ContactMessages
                .OrderByDescending(m => m.Id)
                .ToList();

            return View(messages);
        }
    }
}