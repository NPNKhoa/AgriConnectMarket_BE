namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record ProductBatchResponseDto(Guid Id, string batchCode, DateTime createdAt, DateTime plantingDate, DateTime harvestDate,
            string season, decimal totalYield, decimal avaibleQuantity, decimal price, string units, ICollection<string> imageUrls);
}
