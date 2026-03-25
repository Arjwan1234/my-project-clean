using Microsoft.AspNetCore.Mvc;
using GAMA.CO5.Models;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            var services = GetAllServices();

            var model = new ServicesViewModel
            {
                PageTitle = "خدماتنا التقنية",
                Services = services
            };

            return View(model);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var service = GetAllServices()
                .FirstOrDefault(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (service == null)
                return NotFound();

            return View(service);
        }

        private List<Service> GetAllServices()
        {
            return new List<Service>
            {
                new Service
                {
                    Id = "cloud",
                    Title = "الحوسبة السحابية",
                    Description = "حلول سحابية مرنة وقابلة للتوسع تساعدك على تطوير أعمالك بسرعة وأمان.",
                    Icon = "☁️",
                    ImageUrl = "/images/services/cloud.jpg",
                    BadgeText = "الأكثر طلباً",
                    BadgeColor = "blue",
                    Features = new List<string>
                    {
                        "AWS, Azure, Google Cloud",
                        "ترحيل التطبيقات",
                        "إدارة البنية التحتية",
                        "تحسين التكلفة"
                    }
                },

                new Service
                {
                    Id = "security",
                    Title = "الأمن السيبراني",
                    Description = "حماية متقدمة لبياناتك وأنظمتك من التهديدات والهجمات الإلكترونية.",
                    Icon = "🔐",
                    ImageUrl = "/images/services/security.jpg",
                    BadgeText = "مهم",
                    BadgeColor = "red",
                    Features = new List<string>
                    {
                        "اختبار الاختراق",
                        "مراقبة 24/7",
                        "إدارة المخاطر",
                        "حماية البنية التحتية"
                    }
                },

                new Service
                {
                    Id = "ai",
                    Title = "الذكاء الاصطناعي",
                    Description = "حلول تعتمد على الذكاء الاصطناعي والتعلم الآلي لتحسين الأداء واتخاذ القرار.",
                    Icon = "🤖",
                    ImageUrl = "/images/services/ai.jpg",
                    BadgeText = "جديد",
                    BadgeColor = "purple",
                    Features = new List<string>
                    {
                        "تحليل البيانات الذكي",
                        "روبوتات المحادثة",
                        "الرؤية الحاسوبية",
                        "التعلم الآلي"
                    }
                },

                new Service
                {
                    Id = "analytics",
                    Title = "تحليل البيانات",
                    Description = "استخراج رؤى دقيقة من بياناتك لاتخاذ قرارات استراتيجية مدروسة.",
                    Icon = "📊",
                    ImageUrl = "/images/services/analytics.jpg",
                    BadgeText = "احترافي",
                    BadgeColor = "green",
                    Features = new List<string>
                    {
                        "لوحات معلومات تفاعلية",
                        "تقارير تحليلية",
                        "التنبؤ والتحليل المتقدم",
                        "تكامل البيانات"
                    }
                },

                new Service
                {
                    Id = "mobile",
                    Title = "تطبيقات الجوال",
                    Description = "تطوير تطبيقات احترافية لأنظمة iOS و Android بأعلى معايير الجودة.",
                    Icon = "📱",
                    ImageUrl = "/images/services/mobile.jpg",
                    BadgeText = "مميز",
                    BadgeColor = "orange",
                    Features = new List<string>
                    {
                        "تصميم UX/UI",
                        "تطوير Native & Hybrid",
                        "تكامل API",
                        "نشر على المتاجر"
                    }
                },

                new Service
                {
                    Id = "web",
                    Title = "تطوير المواقع",
                    Description = "تصميم وتطوير مواقع ويب حديثة وسريعة ومتجاوبة.",
                    Icon = "🌐",
                    ImageUrl = "/images/services/web.jpg",
                    BadgeText = "احترافي",
                    BadgeColor = "blue",
                    Features = new List<string>
                    {
                        "تصميم عصري",
                        "تحسين SEO",
                        "أداء عالي",
                        "حماية وأمان"
                    }
                },

                new Service
                {
                    Id = "devops",
                    Title = "DevOps",
                    Description = "أتمتة عمليات التطوير والتشغيل لزيادة الكفاءة وتقليل الأخطاء.",
                    Icon = "⚙️",
                    ImageUrl = "/images/services/devops.jpg",
                    BadgeText = "تقني",
                    BadgeColor = "purple",
                    Features = new List<string>
                    {
                        "CI/CD Pipelines",
                        "Containerization",
                        "Monitoring & Logging",
                        "Cloud Automation"
                    }
                },

                new Service
                {
                    Id = "consulting",
                    Title = "الاستشارات التقنية",
                    Description = "خبرة استشارية متخصصة لدعم رحلتك في التحول الرقمي.",
                    Icon = "🔧",
                    ImageUrl = "/images/services/consulting.jpg",
                    BadgeText = "استشارة",
                    BadgeColor = "green",
                    Features = new List<string>
                    {
                        "تقييم البنية التحتية",
                        "استراتيجية التحول الرقمي",
                        "تدريب الفرق",
                        "تحسين العمليات"
                    }
                }
            };
        }
    }
}