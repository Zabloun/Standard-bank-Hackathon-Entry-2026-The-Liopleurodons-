using System.ComponentModel.DataAnnotations;

namespace Liopleurodons_Pocket_Business_Helper.Models.ViewModels
{
    /// <summary>
    /// View model for creating and editing products.
    /// Includes the category select-list for the dropdown.
    /// </summary>
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter the product name.")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter the selling price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Display(Name = "Selling Price (ZAR)")]
        public decimal Price { get; set; }

        // Populated by the controller for the category dropdown
        public List<CategorySelectItem> Categories { get; set; } = new();
    }

    public class CategorySelectItem
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    /// <summary>
    /// View model for listing products with their current stock level.
    /// </summary>
    public class ProductListViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CurrentStock { get; set; }

        public string StockLevelCss => CurrentStock == 0 ? "low"
                                     : CurrentStock < 5  ? "medium"
                                                         : "high";
        public string StockLabelPill => CurrentStock == 0 ? "red"
                                      : CurrentStock < 5  ? "amber"
                                                          : "green";
        public string StockLabel => CurrentStock == 0 ? "Out of stock"
                                  : CurrentStock < 5  ? $"Low ({CurrentStock})"
                                                      : $"In stock ({CurrentStock})";

        // Width percentage for the mini stock bar (capped at 100)
        public int StockBarPercent => Math.Min(100, CurrentStock * 10);
    }
}
