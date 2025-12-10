namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record ProductBatchResponseDto(Guid Id, string batchCode, string product, string season, string category, string farm, DateTime createdAt,
DateTime plantingDate, DateTime? harvestDate, decimal totalYield, decimal avaibleQuantity, decimal price, string units, ICollection<string> imageUrls);
}
