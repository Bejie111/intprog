using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreadBee.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public UserManagementController(BreadBeeDbContext context)
        {
            _context = context;
        }

        // LIST ALL USERS
        public IActionResult Index(string search)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
                return RedirectToAction("Products", "Product");

            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u =>
                    u.FullName.Contains(search) ||
                    u.Email.Contains(search));
            }

            return View(users.ToList());
        }
        // DELETE USER
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // EDIT ROLE PAGE
        public IActionResult EditRole(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }
        // UPDATE ROLE
        [HttpPost]
        public IActionResult EditRole(User model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);

            if (user == null)
                return NotFound();

            user.Role = model.Role;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
