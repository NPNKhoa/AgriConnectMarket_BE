namespace AgriConnectMarket.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthenRepository AuthenRepository { get; }
        IProfileRepository ProfileRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
