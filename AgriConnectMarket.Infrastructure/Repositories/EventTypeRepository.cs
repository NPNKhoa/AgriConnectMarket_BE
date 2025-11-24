using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class EventTypeRepository(AppDbContext _dbContext) : Repository<CareEventType>(_dbContext), IEventTypeRepository
    {
    }
}
