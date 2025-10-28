using System.Text.RegularExpressions;

namespace AgriConnectMarket.SharedKernel.Validation
{
    public static class Validators
    {
        private static readonly Regex UsernameRegex =
            new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex PhoneRegex =
            new(@"^\+?[0-9]{7,15}$", RegexOptions.Compiled);

        private static readonly Regex UrlRegex =
            new(@"^https?:\/\/[^\s$.?#].[^\s]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsValidUsername(string Username) => UsernameRegex.IsMatch(Username ?? "");
        public static bool IsValidPhone(string phone) => PhoneRegex.IsMatch(phone ?? "");
        public static bool IsValidUrl(string url) => UrlRegex.IsMatch(url ?? "");
    }
}
