using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Liopleurodons_Pocket_Business_Helper.Models
{
    public class Purchases
    {
        
         public int PurchasesId { get; set; }

        public Product PurchasesProduct { get; set; }

        [Required(ErrorMessage = "Please enter a valid quantity to purchase")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalPriceCalculation()
        {
            decimal totalPrice = PurchasesProduct.Price * Quantity;
            return totalPrice;
        }
    }
}
