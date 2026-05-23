using Liopleurodons_Pocket_Business_Helper.Models;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public class PurchasesRepository : RepositoryBase<Purchases>, IPurchasesRepository
    {
        public PurchasesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
