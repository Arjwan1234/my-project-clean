using Microsoft.AspNetCore.Mvc;
using GAMA.CO5.Models;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            var model = new ContactViewModel
            {
                PageTitle = "تواصل معنا"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(ContactViewModel model)
        {
            model.PageTitle = "تواصل معنا";

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var successModel = new ContactViewModel
            {
                PageTitle = "تواصل معنا",
                IsSuccess = true
            };

            return View("Index", successModel);
        }
    }
}