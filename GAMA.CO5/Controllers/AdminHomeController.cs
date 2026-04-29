using GAMA.CO5.Data;
using GAMA.CO5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminHomeController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Hero = await _context.HomeHeroes.FirstOrDefaultAsync();
            ViewBag.About = await _context.HomeAbouts.FirstOrDefaultAsync();

            var news = await _context.HomeNews
                .OrderByDescending(n => n.NewsDate)
                .ToListAsync();

            return View(news);
        }

        public async Task<IActionResult> EditHero()
        {
            var hero = await _context.HomeHeroes.FirstOrDefaultAsync();

            if (hero == null)
            {
                hero = new HomeHero
                {
                    Title = "نبتكر حلولاً رقمية تصنع الأثر",
                    Subtitle = "حلول تقنية متقدمة تدعم الجهات الحكومية والمؤسسات",
                    BadgeText = "فيديو تعريفي",
                    VideoUrl = null
                };

                _context.HomeHeroes.Add(hero);
                await _context.SaveChangesAsync();
            }

            return View(hero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHero(HomeHero model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var hero = await _context.HomeHeroes.FirstOrDefaultAsync();

            if (hero == null)
            {
                hero = new HomeHero();
                _context.HomeHeroes.Add(hero);
            }

            hero.Title = model.Title;
            hero.Subtitle = model.Subtitle;
            hero.BadgeText = model.BadgeText;

            if (model.VideoFile != null && model.VideoFile.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(hero.VideoUrl))
                    DeleteFile(hero.VideoUrl);

                hero.VideoUrl = await SaveVideoAsync(model.VideoFile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditAbout()
        {
            var about = await _context.HomeAbouts.FirstOrDefaultAsync();

            if (about == null)
            {
                about = new HomeAbout
                {
                    BadgeText = "شريكك الموثوق",
                    Title = "شريكك في التحول الرقمي",
                    Description = "نقدم حلول تقنية متطورة للجهات الحكومية والمؤسسات لتحقيق رؤية المملكة 2030",
                    Stat1Number = "+30",
                    Stat1Text = "عاماً من الخبرة",
                    Stat2Number = "+500",
                    Stat2Text = "مشروع ناجح",
                    Stat3Number = "100%",
                    Stat3Text = "نسبة رضا العملاء"
                };

                _context.HomeAbouts.Add(about);
                await _context.SaveChangesAsync();
            }

            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbout(HomeAbout model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var about = await _context.HomeAbouts.FirstOrDefaultAsync();

            if (about == null)
            {
                about = new HomeAbout();
                _context.HomeAbouts.Add(about);
            }

            about.BadgeText = model.BadgeText;
            about.Title = model.Title;
            about.Description = model.Description;

            about.Stat1Number = model.Stat1Number;
            about.Stat1Text = model.Stat1Text;

            about.Stat2Number = model.Stat2Number;
            about.Stat2Text = model.Stat2Text;

            about.Stat3Number = model.Stat3Number;
            about.Stat3Text = model.Stat3Text;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateNews()
        {
            return View(new HomeNews());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNews(HomeNews model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ImageFile != null && model.ImageFile.Length > 0)
                model.ImageUrl = await SaveImageAsync(model.ImageFile);
            else
                model.ImageUrl = "/images/news/placeholder.jpg";

            _context.HomeNews.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditNews(int id)
        {
            var news = await _context.HomeNews.FindAsync(id);
            if (news == null) return NotFound();

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNews(HomeNews model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var news = await _context.HomeNews.FindAsync(model.Id);
            if (news == null) return NotFound();

            news.Title = model.Title;
            news.Description = model.Description;
            news.NewsDate = model.NewsDate;
            news.LinkUrl = model.LinkUrl;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(news.ImageUrl) &&
                    news.ImageUrl != "/images/news/placeholder.jpg")
                {
                    DeleteFile(news.ImageUrl);
                }

                news.ImageUrl = await SaveImageAsync(model.ImageFile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.HomeNews.FindAsync(id);
            if (news == null) return NotFound();

            return View(news);
        }

        [HttpPost, ActionName("DeleteNews")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNewsConfirmed(int id)
        {
            var news = await _context.HomeNews.FindAsync(id);
            if (news == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(news.ImageUrl) &&
                news.ImageUrl != "/images/news/placeholder.jpg")
            {
                DeleteFile(news.ImageUrl);
            }

            _context.HomeNews.Remove(news);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "news");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return "/images/news/" + fileName;
        }

        private async Task<string> SaveVideoAsync(IFormFile videoFile)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "videos", "home");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(videoFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await videoFile.CopyToAsync(stream);

            return "/videos/home/" + fileName;
        }

        private void DeleteFile(string relativePath)
        {
            var fullPath = Path.Combine(
                _environment.WebRootPath,
                relativePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
            );

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }
    }
}