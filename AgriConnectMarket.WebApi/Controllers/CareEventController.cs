using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using AgriConnectMarket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/care-events")]
    [ApiController]
    public class CareEventController(CareEventService _eventService, ICloudinaryAdapter _cloudService) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateEvent([FromForm] CreateCareEventRequest request, CancellationToken ct)
        {
            string url = string.Empty;

            if (request.ImageUrl is not null)
            {
                var res = await _cloudService.UploadAsync(request.ImageUrl);

                url = res.Url ?? string.Empty;
            }

            var dto = new CreateCareEventDto()
            {
                BatchId = request.BatchId,
                EventTypeId = request.EventTypeId,
                Payload = request.Payload,
                ImageUrl = url
            };

            var result = await _eventService.CreateCareEventAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
