using BreadBee.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreadBee.Controllers
{
    public class AdminController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public AdminController(BreadBeeDbContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");
            
            if(role != "Admin")
            {
                return RedirectToAction("Products", "Product");
            }

            var order = _context.Orders
            .Include(o => o.Items)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

            var totalSales = order.Sum(o => o.FinalAmount);

            ViewBag.TotalSales = totalSales;
            ViewBag.TotalOrders = order.Count();

            return View(order);
        }
    }
}
