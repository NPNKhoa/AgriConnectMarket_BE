using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Settings;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class GHTKShippingService : IShippingService
    {
        private readonly HttpClient _httpClient;
        private readonly GHTKOptions _option;

        public GHTKShippingService(HttpClient httpClient, IOptions<GHTKOptions> options)
        {
            _httpClient = httpClient;
            _option = options.Value;
        }

        public async Task<decimal> CalculateShippingFeeAsync(
            string fromProvince,
            string fromDistrict,
            string toProvince,
            string toDistrict,
            int weight = 1)
        {
            var query = new Dictionary<string, string>
            {
                ["pick_province"] = fromProvince,
                ["pick_district"] = fromDistrict,
                ["province"] = toProvince,
                ["district"] = toDistrict,
                ["weight"] = (weight * 1000).ToString()  // grams
            };

            var url = QueryHelpers.AddQueryString(_httpClient.BaseAddress!.ToString(), query);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Token", _option.Token);

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(content);

            var root = doc.RootElement;

            if (!root.GetProperty("success").GetBoolean())
            {
                var msg = root.GetProperty("message").GetString();
                throw new Exception($"GHTK fee calculation error: {msg}");
            }

            var feeObj = root.GetProperty("fee");
            var feeValue = feeObj.GetProperty("fee").GetInt32(); // or GetDecimal depending on API

            return feeValue;
        }
    }
}
