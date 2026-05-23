using Liopleurodons_Pocket_Business_Helper.Data.DataAccess;
using Liopleurodons_Pocket_Business_Helper.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public IEnumerable<Product> GetAllProductsWithCategoryDetails()
        {
            return _appDbContext.Products
                .Include(m => m.ProductCategory);
        }

        public IEnumerable<Product> GetAllProductsInCategory(string category)
        {
            Category cat = _appDbContext.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == category.ToLower());

            return _appDbContext.Products.Where(p => p.CategoryID == cat.CategoryId);
        }


        ///SORTING
        /// public IEnumerable<Product> GetProductsWithOptions(QueryOptions<Product> options)
        /// {
        ///     IQueryable<Product> query = _appDbContext.Products;
        ///     
        ///     if(options.HasWhere)
        ///         query = query.Where(options.Where);
        ///         
        ///     if (options.HasOrderBy)
        ///     {
        ///         if (options.OrderByDirection == "asc")
        ///             query = query.OrderBy(options.OrderBy);
        ///         else
        ///             query = query.OrderByDescending(options.OrderBy);
        ///     }
        ///     
        ///     return query.ToList();
        /// }


    }
}
