using Liopleurodons_Pocket_Business_Helper.Data;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _appDbContext;
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IPurchasesRepository _purchases;
        private IStockRepository _stock;

        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_appDbContext);
                }
                return _product;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_appDbContext);
                }
                return _category;
            }
        }

        public IPurchasesRepository Purchases 
        {
            get
            {
                if (_purchases == null)
                {
                    _purchases = new PurchasesRepository(_appDbContext);
                }
                return _purchases;
            }
        }

        public IStockRepository Stock 
        {
            get
            {
                if (_stock == null)
                {
                    _stock = new StockRepository(_appDbContext);
                }
                return _stock;
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
