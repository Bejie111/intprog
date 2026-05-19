# BreadBee

Simple ASP.NET Core (.NET 10) e-commerce sample (MVC controllers + Razor views) for a bakery storefront with cart/session support and database seeding.

## Features
- Product catalog (Products, Categories)
- Shopping cart stored in session, sidebar cart UI and Checkout flow
- Orders with order items and discounts for logged-in users
- Basic user model with seeded admin account
- EF Core migrations + database seeding on startup
- Static assets (wwwroot) with Bootstrap & jQuery

## Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or other accessible SQL Server instance)
- Optional: dotnet-ef tool for applying migrations (install with `dotnet tool install --global dotnet-ef`)

## Quick start (development)
1. Clone repository
   git clone https://github.com/Bejie111/intprog
2. Configure database
   - Open `appsettings.json` and set `ConnectionStrings:DefaultConnection` to a valid SQL Server connection string. Example:
	 `Server=(localdb)\\mssqllocaldb;Database=BreadBeeDb;Trusted_Connection=True;MultipleActiveResultSets=true`
3. Restore and build
   dotnet restore
   dotnet build
4. Apply EF migrations (optional)
   dotnet ef database update
   The app also runs seed logic at startup to insert sample categories, products, and a default admin user.
5. Run the app
   dotnet run
6. Open the URL printed by the app (usually http://localhost:5000 or http://localhost:5218)

## Seed data / credentials
- Seeded sample admin user (development only):
  - Email: `admin@gmail.com`
  - Password: `123`
- Seeded sample categories and products are added on first run.

## Notes & TODOs
- Passwords are stored in plaintext for the sample. Replace with proper hashing before any real deployment.
- PhoneNumber is required on the User model; the seed uses a placeholder value. Consider making it optional or prompting for a real value.
- Sessions are used for the cart. Ensure cookies are enabled in the browser when testing.

## Important files
- `Program.cs` — app startup, middleware, and seeding
- `Data/BreadBeeDbContext.cs` — EF Core DbContext
- `Data/SeedData.cs` — seeding logic
- `Controllers/` — MVC controllers (Home, Product, Cart, Account, Admin, etc.)
- `Views/` — Razor views and shared layouts
- `wwwroot/` — static assets (css, js, images)
- `Models/` — domain models: Product, Category, User, Cart, Order, OrderItem

## Contributing
Fork, create a branch, add tests where applicable, open a pull request.

## License
No license file included. Add a LICENSE if you plan to publish or distribute this project.