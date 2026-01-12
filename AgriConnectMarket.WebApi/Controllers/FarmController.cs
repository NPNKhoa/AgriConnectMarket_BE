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
    public class FarmController(FarmService _farmService, CertificateService _certificateService, StatisticService _statService, ICloudinaryAdapter _cloudinaryService) : ControllerBase
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

        [HttpGet("/api/account/{accountId}/farm")]
        public async Task<IActionResult> GetFarmByFarmer([FromRoute] Guid accountId, CancellationToken ct)
        {
            var result = await _farmService.GetFarmsByAccountAsync(accountId, ct);

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
                BatchCodePrefix = request.BatchCodePrefix,
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

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpPatch("{farmId}/ban")]
        public async Task<IActionResult> BanFarm([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _farmService.BanFarm(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
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

        [HttpPost("{farmId}/certificate")]
        public async Task<IActionResult> UploadCertificate([FromRoute] Guid farmId, [FromForm] UploadCertificateCommand request, CancellationToken ct)
        {
            string certificateUrl = string.Empty;

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

        [HttpPut("{farmId}/certificates")]
        public async Task<IActionResult> UpdateCertificate([FromRoute] Guid farmId, [FromForm] UpdateCertificateRequest request, CancellationToken ct)
        {
            string certificateUrl = string.Empty;

            if (request.Certificate is not null)
            {
                var uploadResult = await _cloudinaryService.UploadAsync(request.Certificate, ct);

                if (!uploadResult.Success)
                {
                    return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                }

                certificateUrl = uploadResult.Url ?? string.Empty;
            }

            var result = await _certificateService.UpdateCertificate(farmId, certificateUrl, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpDelete("{farmId}/certificate")]
        public async Task<IActionResult> DeleteCertificate([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _certificateService.DeleteCertificate(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_DELETE_SUCCESS_MESSAGE));
        }

        [HttpPatch("{farmId}/toggle-banned")]
        public async Task<IActionResult> ToggleFarmBanned([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _farmService.ToggleFarmBanned(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.BAN_FARM_SUCCESS));
        }

        [HttpPatch("{farmId}/allow-selling")]
        public async Task<IActionResult> AllowFarmSellProducts([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _farmService.AllowForSell(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.FARM_SELL_ALLOWING_SUCCESS));
        }

        [HttpPatch("{farmId}/mark-as-mall")]
        public async Task<IActionResult> MarkFarmAsMallOne([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _farmService.MarkFarmAsMall(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.FARM_MARKED_MALL));
        }

        [HttpGet("{farmId}/revenue")]
        public async Task<IActionResult> GetFarmRevenue([FromRoute] Guid farmId, [FromQuery] RevenueQuery query, CancellationToken ct)
        {
            var result = await _statService.Revenue(farmId, query, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{farmId}/top-customers")]
        public async Task<IActionResult> GetTopVipCustomer([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _statService.TopCustomers(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{farmId}/top-products")]
        public async Task<IActionResult> GetTopSellingProducts([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _statService.BestSellingProducts(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}

