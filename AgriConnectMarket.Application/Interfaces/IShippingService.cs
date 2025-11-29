namespace AgriConnectMarket.Application.Interfaces
{
    public interface IShippingService
    {
        public Task<decimal> CalculateShippingFeeAsync(string fromProvince, string fromDistrict, string toProvince, string toDistrict, int weight);
    }
}
