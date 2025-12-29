using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.RequestDtos;
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
            var result = await _seasonService.GetAllSeasons(ct);

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

        [HttpGet("farm/{farmId}")]
        public async Task<IActionResult> GetSeasonsByFarm([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _seasonService.GetSeasonsByFarmIdAsync(farmId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateSeason([FromBody] CreateSeasonDto dto, CancellationToken ct)
        {
            var result = await _seasonService.CreateSeason(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }

        [HttpPut("{seasonId}")]
        public async Task<IActionResult> UpdateSeason([FromRoute] Guid seasonId, [FromBody] UpdateSeasonDto dto, CancellationToken ct)
        {
            var result = await _seasonService.UpdateSeasonAsync(seasonId, dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpPatch("{seasonId}/status")]
        public async Task<IActionResult> UpdateSeasonStatus([FromRoute] Guid seasonId, [FromBody] UpdateSeasonStatusDto dto, CancellationToken ct)
        {
            var result = await _seasonService.UpdateSeasonStatusAsync(seasonId, dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpDelete("{seasonId}")]
        public async Task<IActionResult> UpdateSeason([FromRoute] Guid seasonId, CancellationToken ct)
        {
            var result = await _seasonService.DeleteAsync(seasonId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_DELETE_SUCCESS_MESSAGE));
        }

        [HttpPatch("{seasonId}")]
        public async Task<IActionResult> CloseSeason([FromRoute] Guid seasonId, CancellationToken ct)
        {
            var result = await _seasonService.CloseSeasonAsync(seasonId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.CLOSE_SEASON_SUCCESS));
        }
    }
}
