namespace AgriConnectMarket.Application.DTOs.QueryDtos
{
    public record CalculateShippingFeeQuery(Guid farmId, Guid addressId, int weight = 1);
}
