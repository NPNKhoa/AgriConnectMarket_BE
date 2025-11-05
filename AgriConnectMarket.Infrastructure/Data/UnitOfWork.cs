using AgriConnectMarket.Application.Interfaces;

namespace AgriConnectMarket.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAuthenRepository AuthenRepository { get; }
        public IProfileRepository ProfileRepository { get; }
        public IFarmRepository FarmRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork
            (
                AppDbContext context,
                IAuthenRepository authenRepository,
                IProfileRepository profileRepository,
                IFarmRepository farmRepository,
                IAddressRepository addressRepository,
                ICategoryRepository categoryRepository
            )
        {
            _context = context;

            AuthenRepository = authenRepository;
            ProfileRepository = profileRepository;
            FarmRepository = farmRepository;
            AddressRepository = addressRepository;
            CategoryRepository = categoryRepository;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }

}
