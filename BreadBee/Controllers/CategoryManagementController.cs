using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreadBee.Controllers
{
    public class CategoryManagementController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public CategoryManagementController(BreadBeeDbContext context)
        {
            _context = context;
        }
        public IActionResult CategoryManagement()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
                return RedirectToAction("Login", "Account");

            var categories = _context.Categories
                .ToList();

            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            _context.Categories.Add(model);
            _context.SaveChanges();

            return RedirectToAction("CategoryManagement");
        }
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == model.Id);

            if (category == null)
                return NotFound();

            category.Name = model.Name;

            _context.SaveChanges();

            return RedirectToAction("CategoryManagement");
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            // safety check (important)
            var hasProducts = _context.Products.Any(p => p.CategoryId == id);

            if (hasProducts)
            {
                return Content("Cannot delete category with products.");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("CategoryManagement");
        }
    }
}
