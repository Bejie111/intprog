using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BreadBee.Controllers
{
    public class ProductManagementController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public ProductManagementController(BreadBeeDbContext context)
        {
            _context = context;
        }

        // LIST PRODUCTS
        public IActionResult Index(string search)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
                return RedirectToAction("Products", "Product");

            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }

            ViewBag.Search = search;


            return View(products.ToList());
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors);

                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewBag.Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                return View(model);
            }

            string fileName = null;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory(),
                                           "wwwroot/uploads",
                                           fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
            }

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                CategoryId = model.CategoryId,
                ImageUrl = "/uploads/" + fileName   
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT PAGE
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();
            ViewBag.Categories = _context.Categories
           .Select(c => new SelectListItem
           {
               Value = c.Id.ToString(),
               Text = c.Name
           })
           .ToList();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model, IFormFile ImageFile)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == model.Id);

            if (product == null)
                return NotFound();

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.CategoryId = model.CategoryId;

            // ✅ ONLY CHANGE IMAGE IF NEW ONE EXISTS
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory(),
                                           "wwwroot/uploads",
                                           fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                product.ImageUrl = "/uploads/" + fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}