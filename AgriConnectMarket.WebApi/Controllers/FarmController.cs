using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using AgriConnectMarket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/farms")]
    [ApiController]
    public class FarmController(FarmService _farmService, CertificateService _certificateService, ICloudinaryAdapter _cloudinaryService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] FarmQuery? query, CancellationToken ct)
        {
            var result = await _farmService.GetAllFarms(query, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpGet("{farmId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _farmService.GetFarmById(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUserFarm(CancellationToken ct)
        {
            var result = await _farmService.GetMyFarm(ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateFarm([FromForm] CreateFarmRequest request, CancellationToken ct)
        {
            string bannerUrl = string.Empty;

            // Upload banner to Cloudinary
            if (request.FarmBanner is not null)
            {
                var uploadResult = await _cloudinaryService.UploadAsync(request.FarmBanner, ct);

                if (!uploadResult.Success)
                {
                    return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                }

                bannerUrl = uploadResult.Url ?? string.Empty;
            }

            var dto = new CreateFarmDto()
            {
                FarmName = request.FarmName,
                FarmDesc = request.FarmDesc,
                BannerUrl = bannerUrl ?? "",
                Phone = request.Phone,
                Area = request.Area,
                FarmerId = request.FarmerId,
                Province = request.Province,
                District = request.District,
                Ward = request.Ward,
                Detail = request.Detail,
            };

            var result = await _farmService.CreateFarmAsync(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }

        [HttpPut("{farmId}")]
        public async Task<IActionResult> UpdateFarm([FromRoute] Guid farmId, [FromForm] UpdateFarmRequest request, CancellationToken ct)
        {
            string bannerUrl = string.Empty;

            // Upload avatar to Cloudinary
            if (request.FarmBanner is not null)
            {
                var uploadResult = await _cloudinaryService.UploadAsync(request.FarmBanner, ct);

                if (!uploadResult.Success)
                {
                    return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                }

                bannerUrl = uploadResult.Url ?? string.Empty;
            }

            var dto = new UpdateFarmDto()
            {
                FarmName = request.FarmName,
                FarmDesc = request.FarmDesc,
                BannerUrl = bannerUrl ?? "",
                Phone = request.Phone,
                Area = request.Area,
                Province = request.Province,
                District = request.District,
                Ward = request.Ward,
                Detail = request.Detail,
            };

            var result = await _farmService.UpdateFarmAsync(farmId, dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }

        [HttpDelete("{farmId}")]
        public async Task<IActionResult> DeleteFarm([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _farmService.DeleteFarmAsync(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPost("{farmId}/upload-certificate")]
        public async Task<IActionResult> UploadCertificate([FromRoute] Guid farmId, [FromForm] UploadCertificateCommand request, CancellationToken ct)
        {
            string certificateUrl = string.Empty;

            // Upload banner to Cloudinary
            if (request.Certificate is not null)
            {
                var uploadResult = await _cloudinaryService.UploadAsync(request.Certificate, ct);

                if (!uploadResult.Success)
                {
                    return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                }

                certificateUrl = uploadResult.Url ?? string.Empty;
            }

            var dto = new UploadCertificateDto()
            {
                CertificateUrl = certificateUrl,
                FarmId = farmId
            };

            var result = await _certificateService.UploadCertificate(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }

        [HttpDelete("{farmId}/upload-certificate")]
        public async Task<IActionResult> DeleteCertificate([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _certificateService.DeleteCertificate(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_DELETE_SUCCESS_MESSAGE));
        }
    }
}

