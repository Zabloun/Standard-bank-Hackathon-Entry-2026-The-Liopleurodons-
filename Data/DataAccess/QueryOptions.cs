using System.Linq.Expressions;

namespace Liopleurodons_Pocket_Business_Helper.Data.DataAccess
{
    public class QueryOptions<T>
    {
        // GENERIC
        // This class encapsulates the options for querying the database, such as sorting, filtering, and paging.
        // public properties for sorting, filtering, and paging
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc"; // default
        public Expression<Func<T, bool>> Where { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // read-only properties
        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;

    }
}