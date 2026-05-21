using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreadBee.Controllers
{
    public class UserController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public UserController(BreadBeeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Products", "Product");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateProfile(User model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);

            if (user == null)
                return NotFound();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;


            _context.SaveChanges();

            // update session
            HttpContext.Session.SetString("Name", user.FullName);

            // get role from DB or session
            var role = user.Role;

            if (role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return RedirectToAction("Products", "Product");
        }
    }
}
