using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA.CO5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var partnersFromDb = await _context.Partners
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            var clientsFromDb = await _context.Clients
                .OrderByDescending(c => c.Id)
                .ToListAsync();

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

                Partners = partnersFromDb.Select(p => new PartnerItem
                {
                    Name = p.Name,
                    ImagePath = p.LogoUrl
                }).ToList(),

                Clients = clientsFromDb.Select(c => new ClientItem
                {
                    Name = c.Name,
                    ImagePath = c.LogoUrl,
                    Url = c.WebsiteUrl,
                    IsFeatured = false
                }).ToList()
            };

            return View(model);
        }

        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}