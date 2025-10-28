namespace AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs
{
    public class UploadResultDto
    {
        public bool Success { get; init; }
        public string? PublicId { get; init; }
        public string? Url { get; init; }
        public string? SecureUrl { get; init; }
        public string? Error { get; init; }
    }
}
