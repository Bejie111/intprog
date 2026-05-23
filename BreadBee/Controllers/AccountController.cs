using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreadBee.Controllers
{
    public class AccountController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public AccountController(BreadBeeDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return RedirectToAction("Products", "Product");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u =>
                    u.Email == email &&
                    u.Password == password);

            if (user == null)
            {
                TempData["LoginError"] = "Invalid email or password.";

                return RedirectToAction("Products", "Product");
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("Name", user.FullName);
            HttpContext.Session.SetString("Email", user.Email);

            if (user.Role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return RedirectToAction("Products", "Product");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Products", "Product");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["RegisterError"] = "Invalid Register";

                return RedirectToAction("Products", "Product");
            }

            // OPTIONAL: prevent duplicate email
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                TempData["RegisterError"] = "Email already exists.";

                return RedirectToAction("Products", "Product");
            }

            user.Role = "Customer";

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Account created successfully.";

            return RedirectToAction("Products", "Product");
        }
    }
}
