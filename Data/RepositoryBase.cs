using Liopleurodons_Pocket_Business_Helper.Data;
using Liopleurodons_Pocket_Business_Helper.Data.DataAccess;
using System.Linq;
using System.Linq.Expressions;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbContext;
        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            //Note: The entity must be Set to be deleted.
            //If the entity is not tracked by the context,
            //you may need to attach it first before calling Remove.
        }

        public IEnumerable<T> FindAll()
        {
            return _appDbContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().Where(expression);
            //Note: The expression is a lambda expression that defines the condition for filtering the data.
        }

        public IEnumerable<T> GetWithOptions(QueryOptions<T> options)
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            if (options.HasWhere)
                query = query.Where(options.Where);

            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }

            if (options.HasPaging)
            {
                //Note: Paging is implemented using the Skip and Take methods of LINQ.
                query = query.Skip((options.PageNumber - 1) * options.PageSize)
                             .Take(options.PageSize);
                //Note: The Skip method is used to skip a specified number of elements in the query,
                //and the Take method is used to take a specified number of elements from the query.
            }

            return query.ToList();

        }


        public T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
            //Note: The Find method is used to find an entity with the given primary key values.
        }

        public void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            //Note: The Update method is used to update an existing entity in the database.
        }
    }
}
