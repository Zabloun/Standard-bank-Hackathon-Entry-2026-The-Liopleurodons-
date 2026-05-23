using System.ComponentModel.DataAnnotations;

namespace Liopleurodons_Pocket_Business_Helper.Models.ViewModels
{
    /// <summary>
    /// View model for adjusting stock quantity for a product.
    /// </summary>
    public class StockAdjustViewModel
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the quantity.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be zero or more.")]
        [Display(Name = "Quantity on Hand")]
        public int Quantity { get; set; }
    }

    /// <summary>
    /// View model for the full stock overview list.
    /// </summary>
    public class StockOverviewViewModel
    {
        public List<StockLineViewModel> StockLines { get; set; } = new();

        public int TotalProducts  => StockLines.Count;
        public int LowStockCount  => StockLines.Count(s => s.Quantity > 0 && s.Quantity < 5);
        public int OutOfStockCount => StockLines.Count(s => s.Quantity == 0);
        public decimal TotalStockValue => StockLines.Sum(s => s.StockValue);
    }

    public class StockLineViewModel
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal StockValue => UnitPrice * Quantity;

        public string StatusCss   => Quantity == 0 ? "low" : Quantity < 5 ? "medium" : "high";
        public string StatusPill  => Quantity == 0 ? "red" : Quantity < 5 ? "amber"  : "green";
        public string StatusLabel => Quantity == 0 ? "Out of stock"
                                   : Quantity < 5  ? $"Low ({Quantity} left)"
                                                   : $"{Quantity} in stock";
        public int BarPercent => Math.Min(100, Quantity * 10);
    }
}
