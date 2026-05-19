using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BreadBee.Controllers
{
    public class HomeController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public HomeController(BreadBeeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products
            .OrderByDescending(p => p.Id)
            .ToList();

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
