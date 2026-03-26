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
                PageTitle = "الوظائف",
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
                    Id = "software-engineer",
                    Title = "مهندس برمجيات",
                    Department = "التطوير",
                    Location = "جدة",
                    Type = "دوام كامل",
                    Description = "نبحث عن مهندس برمجيات محترف للانضمام إلى فريق التطوير."
                },
                new Job
                {
                    Id = "data-scientist",
                    Title = "عالم بيانات",
                    Department = "الذكاء الاصطناعي",
                    Location = "الرياض",
                    Type = "دوام كامل",
                    Description = "نبحث عن عالم بيانات لتحليل البيانات واستخراج الرؤى الذكية."
                },
                new Job
                {
                    Id = "network-engineer",
                    Title = "مهندس شبكات",
                    Department = "البنية التقنية",
                    Location = "جدة",
                    Type = "دوام كامل",
                    Description = "نبحث عن مهندس شبكات بخبرة في البنية التحتية والاتصالات."
                },
                new Job
                {
                    Id = "cloud-architect",
                    Title = "مهندس معماري سحابي",
                    Department = "الحوسبة السحابية",
                    Location = "الرياض",
                    Type = "دوام كامل",
                    Description = "نبحث عن مهندس معماري سحابي لتصميم حلول سحابية متقدمة."
                }
            };
        }
    }
}