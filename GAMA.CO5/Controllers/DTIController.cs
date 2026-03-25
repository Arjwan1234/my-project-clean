using GAMA.CO5.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class DTIController : Controller
    {
        public IActionResult Index()
        {
            var model = new DTIViewModel
            {
                PageTitle = "مؤشر التحول الرقمي"
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Calculate(DTIForm form)
        {
            var score = (form.CloudUsage + form.DigitalStrategy + form.CyberSecurity) / 3;

            string level;
            string recommendation;

            if (score < 30)
            {
                level = "منخفض";
                recommendation = "مستوى التحول الرقمي منخفض. نوصي ببدء رحلة التحول الرقمي مع خدماتنا الاستشارية.";
            }
            else if (score < 60)
            {
                level = "متوسط";
                recommendation = "مستوى التحول الرقمي متوسط. يمكننا مساعدتك في تسريع التحول الرقمي.";
            }
            else
            {
                level = "جيد";
                recommendation = "مستوى التحول الرقمي جيد! نوصي بالاستمرار والتطوير المستمر.";
            }

            var model = new DTIViewModel
            {
                PageTitle = "مؤشر التحول الرقمي",
                Form = form,
                Result = new DTIResult
                {
                    Score = score,
                    Level = level,
                    Recommendation = recommendation
                }
            };

            return View("Index", model);
        }
    }
}
