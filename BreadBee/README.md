# BreadBee

BreadBee is a small ASP.NET Core (.NET 10) bakery storefront sample using MVC controllers with Razor views. It demonstrates a simple product catalog, session-based shopping cart, checkout flow, and EF Core-backed persistence with initial seeding.

## Quick overview
- MVC controllers + Razor views
- EF Core (SQL Server) with migrations
- Session-backed shopping cart and sidebar UI
- Simple Order and User models with seeded admin user

## Prerequisites
- .NET 10 SDK
- SQL Server or LocalDB
- (Optional) dotnet-ef tool for migrations

## Setup & run (development)
1. Clone: git clone https://github.com/Bejie111/intprog
2. Update connection string: open `appsettings.json` and set `ConnectionStrings:DefaultConnection`.
   Example: `Server=(localdb)\\mssqllocaldb;Database=BreadBeeDb;Trusted_Connection=True;MultipleActiveResultSets=true`
3. Restore & build: `dotnet restore` && `dotnet build`
4. (Optional) Apply migrations: `dotnet ef database update`
   - The app also seeds sample categories, products and an admin user on startup via `SeedData`.
5. Run: `dotnet run` (or run from Visual Studio). Browse to the URL printed by the app.

## Seeded data (development)
- Admin: `admin@gmail.com` / `123` (development only)
- Sample categories and products are inserted automatically on first run.

## Key files
- `Program.cs` - startup, middleware and seeding
- `Data/BreadBeeDbContext.cs` - EF Core DbContext
- `Data/SeedData.cs` - seed logic
- `Controllers/` - MVC controllers (Cart, Product, Account, etc.)
- `Views/` - Razor views and shared layouts
- `wwwroot/` - static assets (css, js, images)

