namespace AgriConnectMarket.SharedKernel.Normalization
{
    public static class Normalizer
    {
        public static string NormalizeEmail(string Email)
        {
            return Email?.Trim().ToLowerInvariant() ?? string.Empty;
        }

        public static string NormalizeUsername(string Username)
        {
            return Username?.Trim().ToLowerInvariant() ?? string.Empty;
        }

        public static string NormalizePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;
            return new string(phone.Where(char.IsDigit).ToArray()); // keep digits only
        }

        public static string NormalizeUrl(string url)
        {
            return url?.Trim().ToLowerInvariant() ?? string.Empty;
        }
    }
}
