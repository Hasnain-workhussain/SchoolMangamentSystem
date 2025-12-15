using Microsoft.AspNetCore.Mvc;
using SchoolMangamentSystem.Data;
using SchoolMangamentSystem.ViewModels; // Contains Admin and LoginViewModel
using System.Linq;
using System.IO;                  // Required for MemoryStream
using Microsoft.AspNetCore.Http;  // Required for IFormFile
using System.Threading.Tasks;     // Required for async methods

namespace SchoolMangamentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly SchoolDbContext _context;

        public AccountController(SchoolDbContext context)
        {
            _context = context;
        }

        // ==========================
        // LOGIN SECTION
        // ==========================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = _context.Admins
                    .FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);

                if (admin != null)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            ViewData["ErrorMessage"] = "Invalid email or password!";
            return View(model);
        }

        // ==========================
        // REGISTER SECTION
        // ==========================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Admins.Add(admin);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(admin);
        }

        // ==========================
        // PROFILE SECTION
        // ==========================

        [HttpGet]
        public IActionResult Profile()
        {
            // Fetch the first admin (since we don't have full session tracking yet)
            var admin = _context.Admins.FirstOrDefault();
            if (admin == null) return RedirectToAction("Login");

            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(Admin model, string NewPassword, string ConfirmPassword, IFormFile? file)
        {
            var admin = _context.Admins.Find(model.Id);
            if (admin == null) return NotFound();

            // 1. Handle Image Upload
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    admin.ProfilePicture = memoryStream.ToArray();
                }
            }

            // 2. Update Basic Info
            admin.FullName = model.FullName;
            admin.Email = model.Email;

            // 3. Update Password Logic
            if (!string.IsNullOrEmpty(NewPassword))
            {
                if (NewPassword == ConfirmPassword)
                {
                    admin.Password = NewPassword;
                    ViewData["SuccessMessage"] = "Profile and password updated successfully!";
                }
                else
                {
                    ViewData["ErrorMessage"] = "New passwords do not match.";
                    return View("Profile", admin);
                }
            }
            else
            {
                ViewData["SuccessMessage"] = "Profile updated successfully!";
            }

            _context.SaveChanges();
            return View("Profile", admin);
        }

        // ==========================
        // LOGOUT SECTION
        // ==========================

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}