using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/farms")]
    [ApiController]
    public class FarmController(FarmService _farmService) : ControllerBase
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
        public async Task<IActionResult> BetById([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _farmService.GetFarmById(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }
    }
}
