using BreadBee.Models;

namespace BreadBee.Data
{
    public class SeedData
    {
        public static void Seed(BreadBeeDbContext db)
        {
            if (!db.Categories.Any())
            {
                db.Categories.AddRange(
                    new Category { Name = "Bread" },
                    new Category { Name = "Donut" },
                    new Category { Name = "Pastry" }
                );

                db.SaveChanges();
            }
            if (!db.Products.Any())
            {
                var bread = db.Categories.First(c => c.Name == "Bread");
                var donut = db.Categories.First(c => c.Name == "Donut");
                var pastry = db.Categories.First(c => c.Name == "Pastry");

                db.Products.AddRange(
                    new BreadBee.Models.Product
                    {
                        Name = "Cheese Bread",
                        Description = "Soft bread with cheese filling",
                        Price = 25,
                        Stock = 100,
                        CategoryId = bread.Id,
                        ImageUrl = "/images/cheesebread.jpg"
                    },

                    new BreadBee.Models.Product
                    {
                        Name = "Chocolate Donut",
                        Description = "Sweet chocolate coated donut",
                        Price = 30,
                        Stock = 50,
                        CategoryId = donut.Id,
                        ImageUrl = "/images/donut.jpg"
                    },

                    new BreadBee.Models.Product
                    {
                        Name = "Croissant",
                        Description = "Buttery flaky pastry",
                        Price = 40,
                        Stock = 70,
                        CategoryId = pastry.Id,
                        ImageUrl = "/images/croissant.jpg"
                    }
                );

                db.SaveChanges();
            }

            if (!db.Users.Any())
            {
                db.Users.Add(new BreadBee.Models.User
                {
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    Password = "123",
                    PhoneNumber = "000-000-0000",
                    Role = "Admin"
                });

                db.SaveChanges();
            }
        }
    }
}
