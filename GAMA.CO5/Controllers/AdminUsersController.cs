using GAMA.CO5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("AdminUsers")]
    public class AdminUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private const string MAIN_ADMIN = "admin@gama.com";

        public AdminUsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string email, string password, string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "الإيميل مطلوب");
                return View();
            }

            email = email.Trim().ToLower();

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                existingUser.EmailConfirmed = true;
                existingUser.IsApproved = true;
                existingUser.LockoutEnd = null;
                existingUser.LockoutEnabled = true;
                existingUser.PhoneNumber = phoneNumber;

                await _userManager.UpdateAsync(existingUser);

                if (!await _userManager.IsInRoleAsync(existingUser, "Admin"))
                {
                    await _userManager.AddToRoleAsync(existingUser, "Admin");
                }

                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "كلمة المرور مطلوبة");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsApproved = true,
                LockoutEnabled = true,
                LockoutEnd = null
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                TempData["Success"] = "تم إنشاء المستخدم بنجاح";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        [HttpPost("Approve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsApproved = true;
            user.EmailConfirmed = true;
            user.LockoutEnabled = true;
            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            TempData["Success"] = "تمت الموافقة على المستخدم بنجاح";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Reject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (user.Email == MAIN_ADMIN)
            {
                TempData["Error"] = "لا يمكن رفض الأدمن الأساسي";
                return RedirectToAction(nameof(Index));
            }

            user.IsApproved = false;
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;

            await _userManager.UpdateAsync(user);

            TempData["Success"] = "تم رفض المستخدم";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Disable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (user.Email == MAIN_ADMIN)
            {
                TempData["Error"] = "لا يمكن تعطيل الأدمن الأساسي";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && currentUser.Id == user.Id)
            {
                TempData["Error"] = "لا يمكنك تعطيل حسابك الحالي";
                return RedirectToAction(nameof(Index));
            }

            user.IsApproved = false;
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Enable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsApproved = true;
            user.EmailConfirmed = true;
            user.LockoutEnabled = true;
            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}