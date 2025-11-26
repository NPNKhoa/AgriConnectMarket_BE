using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using QRCoder;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class QrCodeGenerator : IQrCodeGenerator
    {
        private readonly ICloudinaryAdapter _cloudinary;
        private readonly CloudinaryOptions _cloudinarySettings;
        private readonly IUnitOfWork _uow;

        public QrCodeGenerator(
            CloudinaryAdapter cloudinary,
            IOptions<CloudinaryOptions> cloudOpts,
            IUnitOfWork uow)
        {
            _cloudinary = cloudinary;
            _cloudinarySettings = cloudOpts.Value;
            _uow = uow;
        }

        public async Task<string> GenerateAndUploadBatchQrAsync(Guid batchId, CancellationToken ct = default)
        {
            // 1) Validate batch
            var batch = await _uow.ProductBatchRepository.GetByIdAsync(batchId, ct);
            if (batch is null)
                throw new ArgumentException("Batch not found");

            // 2) Build the payload URL (this is the ONLY data inside QR)
            string url = $"https://api.agriconnect.com/trace/batches/{batchId}";

            // 3) Create QR PNG bytes
            byte[] png = GenerateQrPng(url);

            // 4) Upload to Cloudinary
            string folder = $"{_cloudinarySettings.Folder}/batch-qrcodes";

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription($"{batchId}.png", new MemoryStream(png)),
                Folder = folder,
                PublicId = batchId.ToString()
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams, ct);

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error);

            // 5) Return Cloudinary public URL
            return uploadResult.SecureUrl.ToString();
        }

        private byte[] GenerateQrPng(string url)
        {
            using var qrGen = new QRCodeGenerator();
            using var data = qrGen.CreateQrCode(url, QRCodeGenerator.ECCLevel.M);
            using var png = new PngByteQRCode(data);
            return png.GetGraphic(20);
        }
    }
}
