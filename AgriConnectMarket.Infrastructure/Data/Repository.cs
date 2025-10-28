using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Specifications;
using AgriConnectMarket.SharedKernel.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FindAsync(new[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Spec implementations
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
            return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
            return await query.CountAsync(cancellationToken);
        }
    }
}
