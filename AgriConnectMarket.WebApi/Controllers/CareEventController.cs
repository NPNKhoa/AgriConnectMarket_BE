using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/care-events")]
    [ApiController]
    public class CareEventController(CareEventService _eventService) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateCareEventDto dto, CancellationToken ct)
        {
            var result = await _eventService.CreateCareEventAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
