using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/favorite-farms")]
    [ApiController]
    public class FavoriteFarmController(FavoriteFarmService _favoriteFarmService) : ControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetMyFavoriteFarms(CancellationToken ct)
        {
            var result = await _favoriteFarmService.GetMyFavoriteFarms(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(result.Value);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddFarmToFavorite([FromBody] UpdateFavoriteFarmDto dto, CancellationToken ct)
        {
            var result = await _favoriteFarmService.AddToListAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("")]
        public async Task<IActionResult> RemoveFarmFromFavorite([FromRoute] UpdateFavoriteFarmDto dto, CancellationToken ct)
        {
            var result = await _favoriteFarmService.RemoveFromListAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(result.Value);
        }
    }
}
