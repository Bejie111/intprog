using BreadBee.Data;
using BreadBee.Models;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace BreadBee.Controllers
{
    public class CartController : Controller
    {
        private readonly BreadBeeDbContext _context;

        public CartController(BreadBeeDbContext context)
        {
            _context = context;
        }

        public IActionResult Cart()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult Add(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new Cart
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl
                });
            }

            SaveCart(cart);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        private List<Cart> GetCart()
        {
            var session = HttpContext.Session.GetString("Cart");

            if (session == null)
                return new List<Cart>();

            return JsonSerializer.Deserialize<List<Cart>>(session);
        }

        private void SaveCart(List<Cart> cart)
        {
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }
        public IActionResult Remove(int id)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Products", "Product");
        }
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Products", "Product");
        }
        public IActionResult CheckOut()
        {
            var cart = GetCart();

            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Cart");
            }

            return View(cart);
        }

        [HttpPost]
        public IActionResult PlaceOrder(string CustomerName, string Address,string ContactNumber)
        {
            var cart = GetCart();

            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Cart", "Cart");
            }

            var userId = HttpContext.Session.GetInt32("UserId");

            string phone = null;

            if (userId != null)
            {
                var users = _context.Users.FirstOrDefault(u => u.Id == userId);
                phone = users?.PhoneNumber;
            }
            else
            {
                phone = ContactNumber; // guest input
            }

            decimal subtotal = cart.Sum(c => c.Price * c.Quantity);
            int qty = cart.Sum(c => c.Quantity);

            decimal discount = 0;

            // DISCOUNT RULE (only for logged-in users)
            if (userId != null)
            {
                if (qty >= 100)
                    discount = subtotal * 0.20m;
                else if (qty >= 50)
                    discount = subtotal * 0.10m;
                else
                    discount = subtotal * 0.05m;
            }

            var user = userId != null
            ? _context.Users.FirstOrDefault(u => u.Id == userId)
            : null;

            var order = new Order
            {
                UserId = userId,
                CustomerName = user != null ? user.FullName : CustomerName,
                Address = Address,
                ContactNumber = user?.PhoneNumber ?? ContactNumber,
                TotalAmount = subtotal,
                Discount = discount,
                FinalAmount = subtotal - discount,
                OrderDate = DateTime.Now,
                Items = new List<OrderItem>()
            };

            foreach (var item in cart)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

                _context.Orders.Add(order);

            foreach (var item in cart)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);

                if (product.Stock < item.Quantity)
                {
                    return Content($"Not enough stock for {product.Name}");
                }
                if (product != null)
                {
                    product.Stock -= item.Quantity;

                    // safety check (optional but recommended)
                    if (product.Stock < 0)
                        product.Stock = 0;
                }
            }
            _context.SaveChanges();
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Receipt", "Cart", new { id = order.Id });
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }
        public IActionResult Receipt(int id)
        {
            var order = _context.Orders
            .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            _context.Entry(order).Collection(o => o.Items).Load();

            var products = _context.Products.ToDictionary(p => p.Id, p => p.Name);

            foreach (var item in order.Items)
            {
                item.ProductName = products[item.ProductId];
            }
            return View(order);
        }
        public IActionResult Increase(int id) 
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == id);
            if(item != null)
            {
                item.Quantity++;
                SaveCart(cart);
            }
            return RedirectToAction("Products", "Product");
        }

        public IActionResult Decrease(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null) 
            {
                item.Quantity--;
                if (item.Quantity <= 0) 
                {
                    cart.Remove(item);
                }

                SaveCart(cart);
            }
            return RedirectToAction("Products", "Product");
        }   

    }
}
