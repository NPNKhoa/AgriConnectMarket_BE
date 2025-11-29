using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Settings;
using System.Text;
using System.Text.Json;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class GHTKShippingService(HttpClient _httpClient, GHTKOptions option) : IShippingService
    {
        public async Task<decimal> CalculateShippingFeeAsync(string fromProvince, string fromDistrict, string toProvince, string toDistrict, int weight = 1)
        {
            var requestBody = new
            {
                pick_province = fromProvince,
                pick_district = fromDistrict,
                province = toProvince,
                district = toDistrict,
                weight,
            };

            var requestJson = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Token", option.Token);

            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);

            return doc.RootElement.GetProperty("fee").GetDecimal();
        }
    }
}
