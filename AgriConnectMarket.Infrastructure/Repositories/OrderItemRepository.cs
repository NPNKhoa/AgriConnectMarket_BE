using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.Infrastructure.Specifications;
using AgriConnectMarket.SharedKernel.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext _dbContext) : base(_dbContext)
        {
        }

        public async Task<IEnumerable<OrderItem>> ListAsync(ISpecification<OrderItem> spec, bool includeProduct = false, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbContext.Set<OrderItem>().AsQueryable(), spec).AsNoTracking();

            if (includeProduct)
            {
                query = query.Include(i => i.Batch)
                                .ThenInclude(b => b.Season)
                                    .ThenInclude(s => s.Product);
            }

            return await query.ToListAsync(ct);
        }
    }
}
