using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Normalization;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CategoryRepository(AppDbContext _dbContext) : Repository<Category>(_dbContext), ICategoryRepository
    {
        public async Task<Category> GetByNameAsync(string categoryName)
        {
            Guard.AgainstNullOrEmpty(categoryName, nameof(categoryName));

            var normalizedName = Normalizer.NormalizeStringToUpper(categoryName);

            return await _dbContext.Set<Category>().FirstOrDefaultAsync(c => c.CategoryName.Trim().ToUpper().Equals(normalizedName));
        }
    }
}
