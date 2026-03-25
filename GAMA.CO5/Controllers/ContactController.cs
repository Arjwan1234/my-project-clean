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
        public IActionResult Submit(ContactForm form)
        {
            if (!ModelState.IsValid)
            {
                var errorModel = new ContactViewModel
                {
                    PageTitle = "تواصل معنا",
                    Form = form
                };
                return View("Index", errorModel);
            }

            // هنا يتم معالجة البيانات (إرسال إلى قاعدة البيانات أو إيميل)
            // مثال فقط:
            Console.WriteLine($"رسالة جديدة من: {form.Name}");
            Console.WriteLine($"البريد: {form.Email}");
            Console.WriteLine($"الموضوع: {form.Subject}");

            var successModel = new ContactViewModel
            {
                PageTitle = "تواصل معنا",
                IsSuccess = true
            };

            return View("Index", successModel);
        }
    }
}
