using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreadBee.Controllers
{
    public class ProductController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public ProductController(BreadBeeDbContext context)
        {
            _context = context;
        }

        public IActionResult Products(int? categoryId, string search)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            var userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.IsLoggedIn = userId != null;

            // FILTER BY CATEGORY
            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            // SEARCH FILTER
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p =>
                    p.Name.Contains(search) ||
                    p.Description.Contains(search));
            }

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;

            return View(products.ToList());
        }
    }
}
