using Felix.Application.Interfaces.Persistence;
using Felix.Application.Interfaces.Services;
using Felix.Domain.Entities;
using Felix.Infrastructure.Persistence.Context;
using Felix.Infrastructure.Persistence.Repositories;

namespace Felix.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IGenericRepository<Customer> _customer = null;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Customer> Customer => _customer ?? new GenericRepository<Customer>(_context);






        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
