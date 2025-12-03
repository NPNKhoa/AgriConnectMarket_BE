using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/ship")]
    [ApiController]
    public class ShippingServiceController(ShippingFeeService _shippingService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetShippingFee([FromQuery] CalculateShippingFeeQuery query, CancellationToken ct)
        {
            var result = await _shippingService.GetShippingFeeAsync(query, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
