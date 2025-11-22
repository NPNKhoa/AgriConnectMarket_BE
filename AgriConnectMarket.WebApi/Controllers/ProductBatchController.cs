using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/product-batches")]
    [ApiController]
    public class ProductBatchController(ProductBatchService _batchService) : ControllerBase
    {
        [HttpGet("season/${seasonId}")]
        public async Task<IActionResult> GetBatchesBySeason([FromRoute] Guid seasonId, CancellationToken ct)
        {
            var result = await _batchService.GetBatchesBySeasonAsync(seasonId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("farm/${farmId}")]
        public async Task<IActionResult> GetBatchesByFarm([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _batchService.GetBatchByFarmIdAsync(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateBatch([FromBody] CreateProductBatchDto dto, CancellationToken ct)
        {
            var result = await _batchService.CreateBatchAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("${batchId}")]
        public async Task<IActionResult> CreateBatch([FromRoute] Guid batchId, [FromBody] UpdateInventoryDto dto, CancellationToken ct)
        {
            var result = await _batchService.UpdateInventoryAsync(batchId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
