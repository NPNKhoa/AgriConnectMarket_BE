using AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AgriConnectMarket.Infrastructure.CloudinarySettings
{
    public class CloudinaryAdapter : ICloudinaryAdapter, IDisposable
    {
        private readonly Cloudinary _client;
        private readonly CloudinaryOptions _settings;
        private bool _disposed;

        public CloudinaryAdapter(IOptions<CloudinaryOptions> options)
        {
            _settings = options.Value;
            var account = new Account(
                _settings.CloudName,
                _settings.ApiKey,
                _settings.ApiSecret
            );
            _client = new Cloudinary(account)
            {
                Api =
            {
                Secure = _settings.Secure
            }
            };
        }

        public async Task<UploadResultDto> UploadAsync(IFormFile file, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return new UploadResultDto { Success = false, Error = "File is empty" };

            // choose a public id - you can use GUID or meaningful path
            var publicId = $"{_settings.Folder?.TrimEnd('/')}/{Guid.NewGuid():N}";

            // create upload params
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = publicId,
                Overwrite = false,
                UseFilename = false,
                UniqueFilename = true,
                Folder = _settings.Folder,
                Transformation = new Transformation().FetchFormat("auto").Quality("auto")
            };

            try
            {
                // Note: CloudinaryDotNet API is synchronous; wrap in Task.Run for non-blocking IO if needed
                var uploadResult = await Task.Run(() => _client.Upload(uploadParams), ct);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK || uploadResult.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return new UploadResultDto
                    {
                        Success = true,
                        PublicId = uploadResult.PublicId,
                        Url = uploadResult.Url?.ToString(),
                        SecureUrl = uploadResult.SecureUrl?.ToString()
                    };
                }

                return new UploadResultDto { Success = false, Error = uploadResult.Error?.Message ?? "Upload failed" };
            }
            catch (Exception ex)
            {
                // log and return friendly message
                return new UploadResultDto { Success = false, Error = ex.Message };
            }
        }

        public async Task<UploadResultDto> UploadAsync(ImageUploadParams uploadParams, CancellationToken ct = default)
        {
            var publicId = $"{_settings.Folder?.TrimEnd('/')}/{Guid.NewGuid():N}";

            try
            {
                // Note: CloudinaryDotNet API is synchronous; wrap in Task.Run for non-blocking IO if needed
                var uploadResult = await Task.Run(() => _client.Upload(uploadParams), ct);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK || uploadResult.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return new UploadResultDto
                    {
                        Success = true,
                        PublicId = uploadResult.PublicId,
                        Url = uploadResult.Url?.ToString(),
                        SecureUrl = uploadResult.SecureUrl?.ToString()
                    };
                }

                return new UploadResultDto { Success = false, Error = uploadResult.Error?.Message ?? "Upload failed" };
            }
            catch (Exception ex)
            {
                // log and return friendly message
                return new UploadResultDto { Success = false, Error = ex.Message };
            }
        }

        public async Task<UploadResultDto> UploadBase64Async(string base64, string fileName, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return new UploadResultDto { Success = false, Error = "Base64 string empty" };

            var bytes = Convert.FromBase64String(base64);
            var publicId = $"{_settings.Folder?.TrimEnd('/')}/{Guid.NewGuid():N}";

            using var ms = new MemoryStream(bytes);

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(fileName, ms),
                PublicId = publicId,
                Folder = _settings.Folder
            };

            try
            {
                var uploadResult = await Task.Run(() => _client.Upload(uploadParams), ct);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK || uploadResult.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return new UploadResultDto
                    {
                        Success = true,
                        PublicId = uploadResult.PublicId,
                        Url = uploadResult.Url?.ToString(),
                        SecureUrl = uploadResult.SecureUrl?.ToString()
                    };
                }
                return new UploadResultDto { Success = false, Error = uploadResult.Error?.Message ?? "Upload failed" };
            }
            catch (Exception ex)
            {
                return new UploadResultDto { Success = false, Error = ex.Message };
            }
        }

        public async Task<bool> DeleteAsync(string publicId, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(publicId)) return false;

            var deletionParams = new DeletionParams(publicId);

            try
            {
                var result = await Task.Run(() => _client.Destroy(deletionParams), ct);
                return result.Result == "ok" || result.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public string GetResourceUrl(string publicId, ImageTransformOptions? transform = null)
        {
            if (string.IsNullOrWhiteSpace(publicId)) return string.Empty;

            var transformation = new Transformation();

            transformation = transformation.FetchFormat("auto").Quality("auto");

            var url = _client.Api.UrlImgUp
                .Transform(transformation)
                .BuildUrl(publicId);

            return url;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
        }
    }
}
