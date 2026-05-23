using System.ComponentModel.DataAnnotations;

namespace Liopleurodons_Pocket_Business_Helper.Models.ViewModels
{
    /// <summary>
    /// View model for recording a purchase (restocking expense).
    /// </summary>
    public class PurchaseCreateViewModel
    {
        public int PurchasesId { get; set; }

        [Required(ErrorMessage = "Please select a product.")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter the quantity purchased.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        // Populated by controller for the dropdown
        public List<ProductSelectItem> Products { get; set; } = new();
    }

    public class ProductSelectItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string DisplayName => $"{ProductName} — R{Price:N2}";
    }

    /// <summary>
    /// View model for the purchases list screen.
    /// </summary>
    public class PurchaseListViewModel
    {
        public int PurchasesId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string DateLabel { get; set; } = string.Empty;
    }

    // ========== CATEGORY VIEW MODELS ==========

    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category description is required.")]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; } = string.Empty;

        // Populated by controller for the list page
        public int ProductCount { get; set; }
    }

    public class CategoryListViewModel
    {
        public List<CategoryViewModel> Categories { get; set; } = new();
    }
}
