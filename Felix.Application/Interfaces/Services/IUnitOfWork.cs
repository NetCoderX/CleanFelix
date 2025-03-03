using Felix.Application.Interfaces.Persistence;
using Felix.Domain.Entities;

namespace Felix.Application.Interfaces.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer>  Customer { get; }

        Task SaveChangesAsync();
    }
}
