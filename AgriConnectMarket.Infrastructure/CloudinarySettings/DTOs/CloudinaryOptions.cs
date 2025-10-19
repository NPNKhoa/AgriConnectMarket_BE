namespace AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs
{
    public class CloudinaryOptions
    {
        public string CloudName { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string ApiSecret { get; set; } = null!;
        public bool Secure { get; set; } = false; // Default for http
        public string? Folder { get; set; } = "/agriconnect";
        public string? UploadPreset
        {
            get; set;
        }
    }
}
