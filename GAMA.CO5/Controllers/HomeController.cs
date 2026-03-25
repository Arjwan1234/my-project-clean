using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAMA.CO5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                PageTitle = "الرئيسية",

                Hero = new HeroSection
                {
                    Title = "مرحباً بكم في GAMA",
                    Description = "حلول تقنية متطورة تحقق رؤيتكم الرقمية",
                    ButtonText = "استكشف خدماتنا",
                    VideoPath = "~/Videos/video_2026-02-17_10-52-27.mp4",
                    ButtonController = "Services",
                    ButtonAction = "Index"
                },

                Promo = new PromoSection
                {
                    Title = "شريكك في\nالتحول الرقمي",
                    Description = "نقدم حلول تقنية متطورة للجهات الحكومية والمؤسسات لتحقيق رؤية المملكة 2030",
                    Stats = new List<StatItem>
                    {
                        new StatItem { Icon = "📈", Value = "+30", Label = "عامًا من الخبرة" },
                        new StatItem { Icon = "🎯", Value = "+500", Label = "مشروع ناجح" },
                        new StatItem { Icon = "🏅", Value = "100%", Label = "نسبة رضا العملاء" }
                    },
                    Features = new List<FeatureItem>
                    {
                        new FeatureItem
                        {
                            Icon = "🚀",
                            Title = "ابتكار مستمر",
                            SubTitle = "حلول تقنية مبتكرة متوافقة مع رؤية المملكة 2030"
                        },
                        new FeatureItem
                        {
                            Icon = "👥",
                            Title = "فريق الخبراء",
                            SubTitle = "فريق من الخبراء والمتخصصين في جميع المجالات التقنية"
                        }
                    }
                },

                Services = new List<ServiceItem>
                {
                    new ServiceItem
                    {
                        Icon = "☁️",
                        Title = "الحوسبة السحابية",
                        Description = "حلول سحابية مرنة وآمنة لتوسيع أعمالك وتقليل التكاليف."
                    },
                    new ServiceItem
                    {
                        Icon = "🔒",
                        Title = "الأمن السيبراني",
                        Description = "رفع الجاهزية الأمنية وحماية البيانات من التهديدات المتقدمة."
                    },
                    new ServiceItem
                    {
                        Icon = "🤖",
                        Title = "الذكاء الاصطناعي",
                        Description = "أتمتة وتحسين العمليات عبر حلول ذكية تعتمد على البيانات."
                    },
                    new ServiceItem
                    {
                        Icon = "📊",
                        Title = "تحليل البيانات",
                        Description = "لوحات مؤشرات وتقارير تساعد في اتخاذ القرار بسرعة ودقة."
                    }
                },

                Partners = new List<PartnerItem>
                {
                    new PartnerItem { Name = "Microsoft", ImagePath = "~/images/Microsoft.png" },
                    new PartnerItem { Name = "HP", ImagePath = "~/images/hp.png" },
                    new PartnerItem { Name = "Palo Alto", ImagePath = "~/images/paloalto.png" },
                    new PartnerItem { Name = "Cisco", ImagePath = "~/images/cisco.png" },
                    new PartnerItem { Name = "Suprema", ImagePath = "~/images/suprema.png" }
                },

                Clients = new List<ClientItem>
                {
                    new ClientItem
                    {
                        Name = "الهيئة العامة للطيران المدني",
                        ImagePath = "~/images/clients/GACA.png",
                        Url = "https://gaca.gov.sa"
                    },
                    new ClientItem
                    {
                        Name = "الاتصالات السعودية",
                        ImagePath = "~/images/clients/stc.png",
                        Url = "https://www.stc.com.sa",
                        IsFeatured = true
                    },
                    new ClientItem
                    {
                        Name = "أمانة جدة",
                        ImagePath = "~/images/clients/امانة جدة.png",
                        Url = "https://www.jeddah.gov.sa"
                    },
                    new ClientItem
                    {
                        Name = "جامعة الملك سعود",
                        ImagePath = "~/images/clients/جامعة الملك سعود.png",
                        Url = "https://ksu.edu.sa"
                    },
                    new ClientItem
                    {
                        Name = "مصرف الراجحي",
                        ImagePath = "~/images/clients/مصرف الراجحي.png",
                        Url = "https://www.alrajhibank.com.sa"
                    }
                }
            };

            return View(model);
        }
    }
}