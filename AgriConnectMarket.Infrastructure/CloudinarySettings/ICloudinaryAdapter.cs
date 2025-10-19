using AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs;
using Microsoft.AspNetCore.Http;

namespace AgriConnectMarket.Infrastructure.CloudinarySettings
{
    public interface ICloudinaryAdapter
    {
        Task<UploadResultDto> UploadAsync(IFormFile file, CancellationToken ct = default);
        Task<UploadResultDto> UploadBase64Async(string base64, string fileName, CancellationToken ct = default);
        Task<bool> DeleteAsync(string publicId, CancellationToken ct = default);
        string GetResourceUrl(string publicId, ImageTransformOptions? transform = null);
    }
}
