namespace AgriConnectMarket.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthenRepository AuthenRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IFarmRepository FarmRepository { get; }
        IAddressRepository AddressRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISeasonRepository SeasonRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductBatchRepository ProductBatchRepository { get; }
        IFavoriteFarmRepository FavoriteFarmRepository { get; }
        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IPreOrderRepository PreOrderRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
