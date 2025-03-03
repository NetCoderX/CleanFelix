using Felix.Application.Interfaces.Persistence;
using Felix.Domain.Entities;
using Felix.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Felix.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _entity;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() 
        { 
            var response = await _entity.Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null).ToListAsync();

            return response!;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var response = await _entity.SingleOrDefaultAsync(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            return response!;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            entity.AuditCreateUser = 1;
            entity.AuditCreateDate = DateTime.Now;
            entity.State = 1;

            await _dbContext.AddAsync(entity);

            var records = await _dbContext.SaveChangesAsync();
            return records > 0; 
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            entity.AuditUpdateUser = 1;
            entity.AuditUpdateDate = DateTime.Now;
             
            _dbContext.Update(entity);
            _dbContext.Entry(entity).Property(x => x.AuditCreateUser).IsModified = false;
            _dbContext.Entry(entity).Property(x => x.AuditCreateDate).IsModified = false;
            
            var records = await _dbContext.SaveChangesAsync();
            return records > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;
            entity.State = 0;

            _dbContext.Update(entity);

            var records = await _dbContext.SaveChangesAsync();
            return records > 0;
        }

    }
}
