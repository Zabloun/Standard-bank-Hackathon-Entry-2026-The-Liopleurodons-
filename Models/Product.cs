using System.ComponentModel.DataAnnotations;

namespace Liopleurodons_Pocket_Business_Helper.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter the name of the product.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter a description of the stock item.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryID { get; set; }  // foreign key property

        [Required(ErrorMessage = "Please select a category for the product.")]

        public Category ProductCategory { get; set; }

        [Required(ErrorMessage = "Please enter the price of the product.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }


    }
}
