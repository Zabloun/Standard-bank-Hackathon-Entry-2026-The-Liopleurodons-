using System.ComponentModel.DataAnnotations;

namespace Liopleurodons_Pocket_Business_Helper.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Category Description is required.")]
        public string CategoryDescription { get; set; }
        
    }
}
