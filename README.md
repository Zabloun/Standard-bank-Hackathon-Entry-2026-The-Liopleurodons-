# Liopleurodons Pocket Business Helper
**Standard Bank Hackathon 2026**

A lightweight ASP.NET Core MVC web app to help spaza shop owners track stock, purchases, and business performance.

---

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)

### Run the app
```bash
dotnet tool install --global dotnet-ef     # if not already installed
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

The app will open at `https://localhost:5001` (or the port shown in your terminal).  
The database (`LiopleurdonsDB.sqlite`) is created automatically, and sample data is seeded on first run.

---

## Bug Fixes Applied (2026-05-24)

### 1. `_Layout.cshtml` was empty
**Problem:** `Views/Shared/_Layout.cshtml` contained no HTML — it didn't call `@RenderBody()`, so every page threw an `InvalidOperationException: RenderBody has not been called`.  
The actual layout HTML was incorrectly placed inside `Views/Home/Index.cshtml`.  
**Fix:** Moved the layout structure into `_Layout.cshtml` (with `@RenderBody()`, navbar, and footer). Replaced `Views/Home/Index.cshtml` with proper page content.

### 2. Missing foreign key properties on `Stock` and `Purchases` models
**Problem:** `Stock.StockProduct` and `Purchases.PurchasesProduct` were navigation-only properties with no corresponding FK column. EF Core couldn't reliably load or relate these, causing null reference exceptions throughout the app.  
**Fix:** Added `public int? ProductId { get; set; }` to both `Stock` and `Purchases`.

### 3. Navigation properties always null (no eager loading)
**Problem:** `RepositoryBase.FindAll()` and `FindByCondition()` returned bare `DbSet<T>` queries with no `.Include()` calls. As a result, `StockProduct`, `PurchasesProduct`, and `ProductCategory` were always `null` in the controllers, causing "—" to display everywhere.  
**Fix:** Overrode `FindAll()` and `FindByCondition()` in `StockRepository`, `PurchasesRepository`, and `ProductRepository` to include the relevant navigation properties via `.Include().ThenInclude()`.

### 4. SeedData never created Stock rows
**Problem:** `SeedData.EnsurePopulated` seeded categories and products but never created any `Stock` records. The Stock page was always empty even after seeding.  
**Fix:** After saving products, the seed method now creates a `Stock` row (quantity = 10) for each product.

### 5. `appsettings.json` used SQL Server LocalDB
**Problem:** The default connection string pointed to `(localdb)\\MSSQLLocalDB` which only works on Windows with Visual Studio installed.  
**Fix:** Changed the default to SQLite (`Data Source=LiopleurdonsDB.sqlite`) and updated `Program.cs` to use `UseSqlite`. SQL Server option is preserved as a comment.

### 6. `BusinessController` used hardcoded dummy data
**Problem:** The dashboard always showed revenue = R190 and expenses = R380 (hardcoded), making profit permanently negative and never reflecting real database state.  
**Fix:** Controller now queries `_repo.Purchases.FindAll()` to calculate real totals and populate the ledger with actual purchase records.

### 7. Old migrations deleted
**Problem:** The existing `Migrations/` folder was generated from the old model (without FK properties on `Stock`/`Purchases`). Running them on the fixed model would fail.  
**Fix:** Migrations folder removed. Run `dotnet ef migrations add InitialCreate && dotnet ef database update` to regenerate.

---

## Project Structure
```
Controllers/
  BusinessController.cs     — Dashboard
  DataControllers.cs        — Stock, Purchases, Categories
  ProductsController.cs     — Product CRUD
  StaticScreensController.cs — ATM, Personal, Stokvel views
Data/
  AppDbContext.cs
  RepositoryBase.cs / RepositoryWrapper.cs
  ProductRepository.cs / StockRepository.cs / PurchasesRepository.cs / CategoryRepository.cs
Models/
  Product.cs / Stock.cs / Purchases.cs / Category.cs
  ViewModels/
Views/
  Business/ Stock/ Products/ Purchases/ Categories/ Home/ Shared/
```
