using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class EventTypeRepository : Repository<CareEventType>, IEventTypeRepository
    {
        public EventTypeRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
