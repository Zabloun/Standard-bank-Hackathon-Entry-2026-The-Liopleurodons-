using Liopleurodons_Pocket_Business_Helper.Data.DataAccess;
using Liopleurodons_Pocket_Business_Helper.Models;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public interface IProductRepository : IRepositoryBase<Product>
    {

        //Repository with functionality specific to the Product entity
        //This interface will be implemented by the ProductRepository class
        //This interface will be used to access the Product repository in the controllers

        IEnumerable<Product> GetAllProductsInCategory(string category);
        IEnumerable<Product> GetAllProductsWithCategoryDetails();

        //SORTING
        //IEnumerable<Product> GetProductsWithOptions(QueryOptions<Product> options);
        //Uses the QueryOptions class to specify query options for retrieving products, such as filtering, sorting, and pagination.

    }
}
