using FireAndForgetHandler.Data;
using Microsoft.EntityFrameworkCore;

namespace FireAndForgetHandler.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public Repository(ApplicationDbContext applicationDbContext) => 
            _applicationDbContext = applicationDbContext;

        public async Task<T> AddAsync(T entity, 
            CancellationToken cancellationToken = default)
        {
            _ = await _applicationDbContext
                .Set<T>()
                .AddAsync(entity, cancellationToken);

            await _applicationDbContext
                .SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(T entity, 
            CancellationToken cancellationToken = default)
        {
            _applicationDbContext
                .Set<T>()
                .Remove(entity);

            _ = await _applicationDbContext
                .SaveChangesAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext
                .Set<T>()
                .FindAsync(new object[] { id }, 
                    cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext
                .Set<T>()
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
