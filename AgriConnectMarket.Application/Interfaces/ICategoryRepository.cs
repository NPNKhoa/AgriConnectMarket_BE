using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<Category> GetByNameAsync(string categoryName);
    }
}
