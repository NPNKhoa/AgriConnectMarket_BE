using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class StatisticController(StatisticService _statService) : ControllerBase
    {
        [HttpGet("product-per-category")]
        public async Task<IActionResult> GetProductPerCategoryStat(CancellationToken ct)
        {
            var result = await _statService.ProductsPerCategory(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetTotalUserByRoleStat(CancellationToken ct)
        {
            var result = await _statService.TotalUsers(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
