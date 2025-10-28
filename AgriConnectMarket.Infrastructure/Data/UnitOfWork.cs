using AgriConnectMarket.Application.Interfaces;

namespace AgriConnectMarket.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAuthenRepository AuthenRepository { get; }
        public IProfileRepository ProfileRepository { get; }

        public UnitOfWork(AppDbContext context,
                          IAuthenRepository authenRepository,
                          IProfileRepository profileRepository)
        {
            _context = context;
            AuthenRepository = authenRepository;
            ProfileRepository = profileRepository;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }

}
