using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/event-types")]
    [ApiController]
    public class CareEventTypeController(EventTypeService _service) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid eventId, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(eventId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateEventTypeDto dto, CancellationToken ct)
        {
            var result = await _service.CreateAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid eventId, CancellationToken ct)
        {
            var result = await _service.DeleteAsync(eventId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
