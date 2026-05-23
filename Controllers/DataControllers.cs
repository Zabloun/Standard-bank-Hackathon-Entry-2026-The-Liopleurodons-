using Liopleurodons_Pocket_Business_Helper.Data;
using Liopleurodons_Pocket_Business_Helper.Models;
using Liopleurodons_Pocket_Business_Helper.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    // ============================================================
    //  STOCK CONTROLLER
    // ============================================================
    public class StockController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        public StockController(IRepositoryWrapper repo) => _repo = repo;

        // GET /Stock
        public IActionResult Index()
        {
            var stockItems = _repo.Stock.FindAll();
            var vm = new StockOverviewViewModel
            {
                StockLines = stockItems.Select(s => new StockLineViewModel
                {
                    StockId      = s.StockId,
                    ProductId    = s.StockProduct?.ProductId ?? 0,
                    ProductName  = s.StockProduct?.ProductName ?? "—",
                    CategoryName = s.StockProduct?.ProductCategory?.CategoryName ?? "—",
                    UnitPrice    = s.StockProduct?.Price ?? 0,
                    Quantity     = s.Quantity
                }).ToList()
            };
            return View(vm);
        }

        // GET /Stock/Edit/5
        public IActionResult Edit(int id)
        {
            var stock = _repo.Stock.FindByCondition(s => s.StockId == id).FirstOrDefault();
            if (stock == null) return NotFound();

            var vm = new StockAdjustViewModel
            {
                StockId             = stock.StockId,
                ProductId           = stock.StockProduct?.ProductId ?? 0,
                ProductName         = stock.StockProduct?.ProductName ?? "—",
                ProductDescription  = stock.StockProduct?.Description ?? string.Empty,
                ProductPrice        = stock.StockProduct?.Price ?? 0,
                CategoryName        = stock.StockProduct?.ProductCategory?.CategoryName ?? "—",
                Quantity            = stock.Quantity
            };
            return View(vm);
        }

        // POST /Stock/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(StockAdjustViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var stock = _repo.Stock.FindByCondition(s => s.StockId == vm.StockId).FirstOrDefault();
            if (stock == null) return NotFound();

            stock.Quantity = vm.Quantity;
            _repo.Stock.Update(stock);
            _repo.Save();

            TempData["Toast"] = "✓ Stock updated!";
            return RedirectToAction(nameof(Index));
        }
    }

    // ============================================================
    //  PURCHASES CONTROLLER
    // ============================================================
    public class PurchasesController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        public PurchasesController(IRepositoryWrapper repo) => _repo = repo;

        // GET /Purchases
        public IActionResult Index()
        {
            var purchases = _repo.Purchases.FindAll();
            var vm = purchases.Select(p => new PurchaseListViewModel
            {
                PurchasesId = p.PurchasesId,
                ProductName = p.PurchasesProduct?.ProductName ?? "—",
                Quantity    = p.Quantity,
                UnitPrice   = p.PurchasesProduct?.Price ?? 0,
                TotalPrice  = p.TotalPrice,
                DateLabel   = "Today" // TODO: store and display actual date
            }).ToList();
            return View(vm);
        }

        // GET /Purchases/RestockCreate
        public IActionResult RestockCreate()
        {
            var vm = new PurchaseCreateViewModel
            {
                Products = GetProductList()
            };
            return View(vm);
        }

        // POST /Purchases/RestockCreate
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult RestockCreate(PurchaseCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Products = GetProductList();
                return View(vm);
            }

            var product = _repo.Product.FindByCondition(p => p.ProductId == vm.ProductId).FirstOrDefault();
            if (product == null)
            {
                ModelState.AddModelError("ProductId", "Product not found.");
                vm.Products = GetProductList();
                return View(vm);
            }

            var purchase = new Purchases
            {
                PurchasesProduct = product,
                Quantity         = vm.Quantity,
                TotalPrice       = product.Price * vm.Quantity
            };
            _repo.Purchases.Create(purchase);

            // Increase stock level
            var stock = _repo.Stock.FindAll().FirstOrDefault(s => s.StockProduct?.ProductId == product.ProductId);
            if (stock != null)
            {
                stock.Quantity += vm.Quantity;
                _repo.Stock.Update(stock);
            }

            _repo.Save();

            TempData["Toast"] = $"✓ Expense logged — R{purchase.TotalPrice:N2}";
            return RedirectToAction("Index", "Business");
        }

        // Reuse RestockCreate view for the "Record Sale" quick action
        // GET /Purchases/Create
        public IActionResult Create()
        {
            var vm = new PurchaseCreateViewModel { Products = GetProductList() };
            return View("RestockCreate", vm);
        }

        // ---- Helpers ----
        private List<ProductSelectItem> GetProductList() =>
            _repo.Product.FindAll()
                 .Select(p => new ProductSelectItem
                 {
                     ProductId   = p.ProductId,
                     ProductName = p.ProductName,
                     Price       = p.Price
                 }).ToList();
    }

    // ============================================================
    //  CATEGORIES CONTROLLER
    // ============================================================
    public class CategoriesController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        public CategoriesController(IRepositoryWrapper repo) => _repo = repo;

        // GET /Categories
        public IActionResult Index()
        {
            var products = _repo.Product.FindAll();
            var vm = new CategoryListViewModel
            {
                Categories = _repo.Category.FindAll()
                    .Select(c => new CategoryViewModel
                    {
                        CategoryId          = c.CategoryId,
                        CategoryName        = c.CategoryName,
                        CategoryDescription = c.CategoryDescription,
                        ProductCount        = products.Count(p => p.CategoryID == c.CategoryId)
                    }).ToList()
            };
            return View(vm);
        }

        // GET /Categories/Create
        public IActionResult Create() => View(new CategoryViewModel());

        // POST /Categories/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _repo.Category.Create(new Category
            {
                CategoryName        = vm.CategoryName,
                CategoryDescription = vm.CategoryDescription
            });
            _repo.Save();

            TempData["Toast"] = $"✓ {vm.CategoryName} created!";
            return RedirectToAction(nameof(Index));
        }

        // GET /Categories/Edit/5
        public IActionResult Edit(int id)
        {
            var cat = _repo.Category.FindByCondition(c => c.CategoryId == id).FirstOrDefault();
            if (cat == null) return NotFound();

            return View(new CategoryViewModel
            {
                CategoryId          = cat.CategoryId,
                CategoryName        = cat.CategoryName,
                CategoryDescription = cat.CategoryDescription
            });
        }

        // POST /Categories/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var cat = _repo.Category.FindByCondition(c => c.CategoryId == vm.CategoryId).FirstOrDefault();
            if (cat == null) return NotFound();

            cat.CategoryName        = vm.CategoryName;
            cat.CategoryDescription = vm.CategoryDescription;
            _repo.Category.Update(cat);
            _repo.Save();

            TempData["Toast"] = "✓ Category updated!";
            return RedirectToAction(nameof(Index));
        }

        // GET /Categories/Delete/5
        public IActionResult Delete(int id)
        {
            var cat = _repo.Category.FindByCondition(c => c.CategoryId == id).FirstOrDefault();
            if (cat == null) return NotFound();

            var productCount = _repo.Product.FindAll().Count(p => p.CategoryID == id);
            return View(new CategoryViewModel
            {
                CategoryId          = cat.CategoryId,
                CategoryName        = cat.CategoryName,
                CategoryDescription = cat.CategoryDescription,
                ProductCount        = productCount
            });
        }

        // POST /Categories/DeleteConfirmed
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cat = _repo.Category.FindByCondition(c => c.CategoryId == id).FirstOrDefault();
            if (cat == null) return NotFound();
            _repo.Category.Delete(cat);
            _repo.Save();
            TempData["Toast"] = "✓ Category deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
