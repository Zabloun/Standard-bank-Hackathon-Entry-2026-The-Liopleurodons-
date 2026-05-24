using Liopleurodons_Pocket_Business_Helper.Models;
using Microsoft.EntityFrameworkCore;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public class PurchasesRepository : RepositoryBase<Purchases>, IPurchasesRepository
    {
        public PurchasesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public IEnumerable<Purchases> FindAllWithProducts()
        {
            return _appDbContext.Purchases
                .Include(p => p.PurchasesProduct)
                .ToList();
        }
    }
}
