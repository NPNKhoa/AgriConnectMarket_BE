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
        public ISeasonRepository SeasonRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductBatchRepository ProductBatchRepository { get; }
        public IFavoriteFarmRepository FavoriteFarmRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public IPreOrderRepository PreOrderRepository { get; }
        public IEventTypeRepository EventTypeRepository { get; }
        public ICareEventRepository CareEventRepository { get; }

        public UnitOfWork
            (
                AppDbContext context,
                IAuthenRepository authenRepository,
                IProfileRepository profileRepository,
                IFarmRepository farmRepository,
                IAddressRepository addressRepository,
                ICategoryRepository categoryRepository,
                ISeasonRepository seasonRepository,
                IProductRepository productRepository,
                IProductBatchRepository productBatchRepository,
                IFavoriteFarmRepository favoriteFarmRepository,
                ICartRepository cartRepository,
                ICartItemRepository cartItemRepository,
                IOrderRepository orderRepository,
                IOrderItemRepository orderItemRepository,
                IPreOrderRepository preOrderRepository,
                IEventTypeRepository eventTypeRepository,
                ICareEventRepository careEventRepository
            )
        {
            _context = context;

            AuthenRepository = authenRepository;
            ProfileRepository = profileRepository;
            FarmRepository = farmRepository;
            AddressRepository = addressRepository;
            CategoryRepository = categoryRepository;
            SeasonRepository = seasonRepository;
            ProductRepository = productRepository;
            ProductBatchRepository = productBatchRepository;
            FavoriteFarmRepository = favoriteFarmRepository;
            CartRepository = cartRepository;
            CartItemRepository = cartItemRepository;
            OrderRepository = orderRepository;
            OrderItemRepository = orderItemRepository;
            PreOrderRepository = preOrderRepository;
            EventTypeRepository = eventTypeRepository;
            CareEventRepository = careEventRepository;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }

}
