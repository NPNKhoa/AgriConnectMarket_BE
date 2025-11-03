namespace AgriConnectMarket.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthenRepository AuthenRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IFarmRepository FarmRepository { get; }
        IAddressRepository AddressRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
