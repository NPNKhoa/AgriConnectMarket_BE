using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class ViolationRepository : Repository<ViolationReport>, IViolationReportRepository
    {
        public ViolationRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
