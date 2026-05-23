using System.ComponentModel.DataAnnotations;

namespace Liopleurodons_Pocket_Business_Helper.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        [Required(ErrorMessage = "Please provide a valid product")]
        public Product StockProduct { get; set; }

        [Required(ErrorMessage = "Please enter the quantity of the stock item.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
        public int Quantity { get; set; }

        

    }
}
