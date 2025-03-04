using Felix.Application.Commons.Bases;
using Felix.Application.Interfaces.Services;
using System.Linq.Dynamic.Core;

namespace Felix.Infrastructure.Services
{
    public class OrderingQuery : IOrderingQuery
    {
        public IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryble) where T : class
        {
            IQueryable<T> query = request.Order == "desc"
                ? queryble.OrderBy($"{request.Sort} descending")
                : queryble.OrderBy($"{request.Sort} ascending");

            query = query.Paginate(request);

            return query;
        }
    }
}
