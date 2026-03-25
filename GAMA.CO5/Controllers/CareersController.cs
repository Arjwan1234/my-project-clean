using Microsoft.AspNetCore.Mvc;
using GAMA.CO5.Models;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class CareersController : Controller
    {
        public IActionResult Index()
        {
            var model = new CareersViewModel
            {
                PageTitle = "انضم لفريقنا",
                Jobs = GetAllJobs()
            };

            return View(model);
        }

        private List<Job> GetAllJobs()
        {
            return new List<Job>
            {
                new Job
                {
                    Id = "fullstack",
                    Title = "مطور Full Stack",
                    Department = "التطوير",
                    Location = "جدة",
                    Type = "دوام كامل",
                    Description = "نبحث عن مطور Full Stack محترف للانضمام لفريقنا"
                },
                new Job
                {
                    Id = "security",
                    Title = "مهندس أمن سيبراني",
                    Department = "الأمن",
                    Location = "الرياض",
                    Type = "دوام كامل",
                    Description = "نبحث عن مهندس أمن سيبراني خبير"
                },
                new Job
                {
                    Id = "analyst",
                    Title = "محلل بيانات",
                    Department = "البيانات",
                    Location = "جدة",
                    Type = "دوام كامل",
                    Description = "نبحث عن محلل بيانات محترف"
                },
                new Job
                {
                    Id = "designer",
                    Title = "مصمم UX/UI",
                    Department = "التصميم",
                    Location = "الرياض",
                    Type = "دوام كامل",
                    Description = "نبحث عن مصمم UX/UI مبدع"
                },
                new Job
                {
                    Id = "pm",
                    Title = "مدير مشاريع تقنية",
                    Department = "الإدارة",
                    Location = "جدة",
                    Type = "دوام كامل",
                    Description = "نبحث عن مدير مشاريع تقنية خبير"
                }
            };
        }
    }
}
