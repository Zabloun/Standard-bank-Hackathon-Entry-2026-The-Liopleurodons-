using Liopleurodons_Pocket_Business_Helper.Models;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(AppDbContext appDbContext) : base(appDbContext)
        {
    }
}
