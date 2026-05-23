namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public interface IRepositoryWrapper
    {

        //The Repository Wrapper interface that will be implemented by the RepositoryWrapper class
        //This interface will be used to access the repositories in the controllers
        //The Repository Wrapper pattern is used to encapsulate the logic for accessing the repositories
        //and to provide a single point of access to the repositories.
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        IPurchasesRepository Purchases { get; }

        IStockRepository Stock { get; }


        void Save();
    }
}
