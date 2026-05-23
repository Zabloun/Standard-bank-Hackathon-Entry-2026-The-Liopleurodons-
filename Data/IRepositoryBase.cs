using Liopleurodons_Pocket_Business_Helper.Data.DataAccess;
using System.Linq.Expressions;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public interface IRepositoryBase<T>
    {
        //Every repository will have their respective Interface
        //This interface will be implemented by the RepositoryBase class
        //and will be inherited by the respective repository interfaces

        //This interface is the papa bear

        //Generic repository interface with basic CRUD operations and query options
        T GetById(int id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetWithOptions(QueryOptions<T> options);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
