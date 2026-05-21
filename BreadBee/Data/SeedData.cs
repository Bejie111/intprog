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
                    new Category { Name = "Pastry" },
                    new Category { Name = "Muffin" },
                    new Category { Name = "Loaf" }
                );
                db.SaveChanges();
            }

            if (!db.Products.Any())
            {
                var bread = db.Categories.First(c => c.Name == "Bread");
                var donut = db.Categories.First(c => c.Name == "Donut");
                var pastry = db.Categories.First(c => c.Name == "Pastry");
                var muffin = db.Categories.First(c => c.Name == "Muffin");
                var loaf = db.Categories.First(c => c.Name == "Loaf");

                db.Products.AddRange(

                    // BREAD
                    new BreadBee.Models.Product
                    {
                        Name = "Cheese Bread",
                        Description = "Soft bread with cheese filling",
                        Price = 25,
                        Stock = 100,
                        CategoryId = bread.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1586444248902-2f64eddc13df?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Pandesal",
                        Description = "Classic Filipino soft bread roll with breadcrumb crust",
                        Price = 8,
                        Stock = 200,
                        CategoryId = bread.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Ensaymada",
                        Description = "Soft coiled bun topped with butter, sugar, and grated cheese",
                        Price = 35,
                        Stock = 80,
                        CategoryId = bread.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Ube Bread",
                        Description = "Purple yam bread with sweet ube halaya filling",
                        Price = 45,
                        Stock = 60,
                        CategoryId = bread.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1571115764595-644a1f56a55c?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Spanish Bread",
                        Description = "Rolled bread filled with sweet buttery breadcrumb filling",
                        Price = 15,
                        Stock = 120,
                        CategoryId = bread.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1555507036-ab1f4038808a?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Monay",
                        Description = "Dense and chewy Filipino bread with a signature slit on top",
                        Price = 12,
                        Stock = 90,
                        CategoryId = bread.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=400"
                    },

                    // DONUT
                    new BreadBee.Models.Product
                    {
                        Name = "Chocolate Donut",
                        Description = "Sweet chocolate coated donut",
                        Price = 30,
                        Stock = 50,
                        CategoryId = donut.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1551024601-bec78aea704b?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Glazed Donut",
                        Description = "Classic ring donut with a shiny sweet glaze",
                        Price = 28,
                        Stock = 60,
                        CategoryId = donut.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1551024601-bec78aea704b?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Strawberry Donut",
                        Description = "Fluffy donut topped with strawberry icing and sprinkles",
                        Price = 32,
                        Stock = 45,
                        CategoryId = donut.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1565299624946-b28f40a0ae38?w=400"
                    },

                    // PASTRY
                    new BreadBee.Models.Product
                    {
                        Name = "Croissant",
                        Description = "Buttery flaky pastry",
                        Price = 40,
                        Stock = 70,
                        CategoryId = pastry.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1555507036-ab1f4038808a?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Cinnamon Roll",
                        Description = "Swirled pastry with cinnamon sugar and cream cheese icing",
                        Price = 55,
                        Stock = 40,
                        CategoryId = pastry.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1534422298391-e4f8c172dddb?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Ham and Cheese Puff",
                        Description = "Flaky puff pastry stuffed with savory ham and melted cheese",
                        Price = 50,
                        Stock = 55,
                        CategoryId = pastry.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1586444248902-2f64eddc13df?w=400"
                    },

                    // MUFFIN
                    new BreadBee.Models.Product
                    {
                        Name = "Blueberry Muffin",
                        Description = "Moist muffin packed with fresh juicy blueberries",
                        Price = 38,
                        Stock = 50,
                        CategoryId = muffin.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1558961363-fa8fdf82db35?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Chocolate Chip Muffin",
                        Description = "Soft muffin loaded with rich chocolate chips throughout",
                        Price = 38,
                        Stock = 50,
                        CategoryId = muffin.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1558961363-fa8fdf82db35?w=400"
                    },

                    // LOAF
                    new BreadBee.Models.Product
                    {
                        Name = "Sourdough Loaf",
                        Description = "Naturally leavened artisan bread with crispy crust and chewy crumb",
                        Price = 120,
                        Stock = 30,
                        CategoryId = loaf.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1586444248897-a00f00d4a2c7?w=400"
                    },
                    new BreadBee.Models.Product
                    {
                        Name = "Banana Loaf",
                        Description = "Moist and tender banana bread loaf with a golden top",
                        Price = 95,
                        Stock = 35,
                        CategoryId = loaf.Id,
                        ImageUrl = "https://images.unsplash.com/photo-1534422298391-e4f8c172dddb?w=400"
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