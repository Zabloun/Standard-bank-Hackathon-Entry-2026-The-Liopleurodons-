using Liopleurodons_Pocket_Business_Helper.Models;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
