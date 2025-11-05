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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
