using Felix.Application.Commons.Bases;

namespace Felix.Application.Interfaces.Services
{
    public interface IOrderingQuery
    {
        IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryble) where T: class;
    }
}
