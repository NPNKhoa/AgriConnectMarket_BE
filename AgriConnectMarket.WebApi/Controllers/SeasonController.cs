using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/seasons")]
    [ApiController]
    public class SeasonController(SeasonService _seasonService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllSeasons([FromQuery] SeasonQuery? query, CancellationToken ct)
        {
            var result = await _seasonService.GetAllSeasons(query, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpGet("{seasonId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid seasonId, CancellationToken ct)
        {
            var result = await _seasonService.GetSeasonById(seasonId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }
    }
}
