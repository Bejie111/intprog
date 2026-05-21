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

        public IActionResult Register(User user)
        {
            if (string.IsNullOrEmpty(user.FullName) ||
               string.IsNullOrEmpty(user.Email) ||
               string.IsNullOrEmpty(user.Password) ||
               string.IsNullOrEmpty(user.PhoneNumber))
            {
                ViewBag.Error = "All fields are required";
                return View();
            }
            user.Role = "Customer";

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Products","Product");
        }
    }
}
