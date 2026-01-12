namespace AgriConnectMarket.Infrastructure.Settings
{
    public class VnPaySettings
    {
        public string Url { get; set; } = null!;
        public string TmnCode { get; set; } = null!;
        public string HashSecret { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
        public string? ClientBaseResultUrl { get; set; }
        public string IpnUrl { get; set; } = null!;
        public string Locale { get; set; } = "vn";
    }
}
