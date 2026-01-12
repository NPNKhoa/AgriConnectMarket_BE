using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using AgriConnectMarket.WebApi.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/violation-reports")]
    [ApiController]
    public class ViolationReportController(ViolationReportService _reportService, ICloudinaryAdapter _cloudService) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateReport([FromForm] AddReportRequest request, CancellationToken ct)
        {
            string url = "";

            if (request.evidenceImage is not null)
            {
                var uploadresult = await _cloudService.UploadAsync(request.evidenceImage, ct);

                if (uploadresult.Success)
                {
                    url = uploadresult.Url!;
                }
            }

            AddViolationReportRequestDto dto = new(request.farmId, request.content, request.violationType, url);

            var result = await _reportService.CreateAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllReports(CancellationToken ct)
        {
            var result = await _reportService.GetAllReportsAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
