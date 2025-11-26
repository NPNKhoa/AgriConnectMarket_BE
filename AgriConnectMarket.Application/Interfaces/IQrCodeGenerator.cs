namespace AgriConnectMarket.Application.Interfaces
{
    public interface IQrCodeGenerator
    {
        Task<string> GenerateAndUploadBatchQrAsync(Guid batchId, CancellationToken ct = default);
    }
}
