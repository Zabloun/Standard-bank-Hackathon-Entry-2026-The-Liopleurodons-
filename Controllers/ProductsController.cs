using Liopleurodons_Pocket_Business_Helper.Data;
using Liopleurodons_Pocket_Business_Helper.Models;
using Liopleurodons_Pocket_Business_Helper.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Liopleurodons_Pocket_Business_Helper.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public ProductsController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET /Products
        public IActionResult Index()
        {
            var products = _repo.Product.FindAll();
            var stockAll = _repo.Stock.FindAll();

            var vm = products.Select(p =>
            {
                var stock = stockAll.FirstOrDefault(s => s.StockProduct?.ProductId == p.ProductId);
                return new ProductListViewModel
                {
                    ProductId    = p.ProductId,
                    ProductName  = p.ProductName,
                    Description  = p.Description,
                    CategoryName = p.ProductCategory?.CategoryName ?? "—",
                    Price        = p.Price,
                    CurrentStock = stock?.Quantity ?? 0
                };
            }).ToList();

            return View(vm);
        }

        // GET /Products/Create
        public IActionResult Create()
        {
            var vm = new ProductViewModel
            {
                Categories = GetCategoryList()
            };
            return View(vm);
        }

        // POST /Products/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = GetCategoryList();
                return View(vm);
            }

            var category = _repo.Category.FindByCondition(c => c.CategoryId == vm.CategoryId).FirstOrDefault();
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Selected category not found.");
                vm.Categories = GetCategoryList();
                return View(vm);
            }

            var product = new Product
            {
                ProductName     = vm.ProductName,
                Description     = vm.Description,
                CategoryID      = vm.CategoryId,
                ProductCategory = category,
                Price           = vm.Price
            };

            _repo.Product.Create(product);
            _repo.Save();

            // Auto-create a stock entry at zero for new products
            _repo.Stock.Create(new Stock { StockProduct = product, Quantity = 0 });
            _repo.Save();

            TempData["Toast"] = $"✓ {vm.ProductName} added!";
            return RedirectToAction(nameof(Index));
        }

        // GET /Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _repo.Product.FindByCondition(p => p.ProductId == id).FirstOrDefault();
            if (product == null) return NotFound();

            var vm = new ProductViewModel
            {
                ProductId   = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                CategoryId  = product.CategoryID,
                Price       = product.Price,
                Categories  = GetCategoryList()
            };
            return View(vm);
        }

        // POST /Products/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = GetCategoryList();
                return View(vm);
            }

            var product = _repo.Product.FindByCondition(p => p.ProductId == vm.ProductId).FirstOrDefault();
            if (product == null) return NotFound();

            var category = _repo.Category.FindByCondition(c => c.CategoryId == vm.CategoryId).FirstOrDefault();

            product.ProductName     = vm.ProductName;
            product.Description     = vm.Description;
            product.CategoryID      = vm.CategoryId;
            product.ProductCategory = category;
            product.Price           = vm.Price;

            _repo.Product.Update(product);
            _repo.Save();

            TempData["Toast"] = "✓ Product updated!";
            return RedirectToAction(nameof(Index));
        }

        // GET /Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _repo.Product.FindByCondition(p => p.ProductId == id).FirstOrDefault();
            if (product == null) return NotFound();

            var stock = _repo.Stock.FindAll().FirstOrDefault(s => s.StockProduct?.ProductId == id);
            var vm = new ProductListViewModel
            {
                ProductId    = product.ProductId,
                ProductName  = product.ProductName,
                Description  = product.Description,
                CategoryName = product.ProductCategory?.CategoryName ?? "—",
                Price        = product.Price,
                CurrentStock = stock?.Quantity ?? 0
            };
            return View(vm);
        }

        // POST /Products/DeleteConfirmed
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _repo.Product.FindByCondition(p => p.ProductId == id).FirstOrDefault();
            if (product == null) return NotFound();

            // Remove associated stock entry first
            var stock = _repo.Stock.FindAll().FirstOrDefault(s => s.StockProduct?.ProductId == id);
            if (stock != null)
            {
                _repo.Stock.Delete(stock);
                _repo.Save();
            }

            _repo.Product.Delete(product);
            _repo.Save();

            TempData["Toast"] = "✓ Product deleted.";
            return RedirectToAction(nameof(Index));
        }

        // ---- Helpers ----
        private List<CategorySelectItem> GetCategoryList() =>
            _repo.Category.FindAll()
                 .Select(c => new CategorySelectItem { CategoryId = c.CategoryId, CategoryName = c.CategoryName })
                 .ToList();
    }
}
