using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using AgriConnectMarket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/product-batches")]
    [ApiController]
    public class ProductBatchController : ControllerBase
    {
        private readonly ProductBatchService _batchService;
        private readonly CareEventService _eventService;
        private readonly ICloudinaryAdapter _cloudinaryAdapter;

        public ProductBatchController(
            ProductBatchService batchService,
            CareEventService eventService,
            ICloudinaryAdapter cloudinaryAdapter)
        {
            _batchService = batchService;
            _eventService = eventService;
            _cloudinaryAdapter = cloudinaryAdapter;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBatches(CancellationToken ct)
        {
            var result = await _batchService.GetAllBatchesAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("selling")]
        public async Task<IActionResult> GetSellingBatches([FromQuery] ProductBatchQuery query, CancellationToken ct)
        {
            var result = await _batchService.GetSellingBatches(query, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{batchId}")]
        public async Task<IActionResult> GetBatchById([FromRoute] Guid batchId, CancellationToken ct)
        {
            var result = await _batchService.GetBatchByIdAsync(batchId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("season/{seasonId}")]
        public async Task<IActionResult> GetBatchesBySeason([FromRoute] Guid seasonId, CancellationToken ct)
        {
            var result = await _batchService.GetBatchesBySeasonAsync(seasonId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("farm/{farmId}")]
        public async Task<IActionResult> GetBatchesByFarm([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _batchService.GetBatchByFarmIdAsync(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("farmer/{accountId}")]
        public async Task<IActionResult> GetBatchesByFarmer([FromRoute] Guid accountId, CancellationToken ct)
        {
            var result = await _batchService.GetBatchesByFarmerAsync(accountId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{batchId}/care-events")]
        public async Task<IActionResult> GetCareEvents([FromRoute] Guid batchId, CancellationToken ct)
        {
            var result = await _batchService.GetBatchByFarmIdAsync(batchId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{batchId}/care-events/verify")]
        public async Task<IActionResult> VerifyChain([FromRoute] Guid batchId, CancellationToken ct)
        {
            var result = await _eventService.GetCareEventsByBatchAsync(batchId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateBatch([FromForm] CreateProductBatchRequest request, CancellationToken ct)
        {
            ICollection<string> urls = [];

            if (request.Images.Any())
            {
                foreach (var item in request.Images)
                {
                    var uploadResult = await _cloudinaryAdapter.UploadAsync(item, ct);

                    if (!uploadResult.Success)
                    {
                        return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                    }

                    urls.Add(uploadResult.Url ?? string.Empty);
                }
            }

            var dto = new CreateProductBatchDto()
            {
                PlantingDate = request.PlantingDate,
                SeasonId = request.SeasonId,
                TotalYield = request.TotalYield,
                Units = request.Units,
                ImageUrl = urls
            };

            var result = await _batchService.CreateBatchAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("{batchId}")]
        public async Task<IActionResult> UpdateInventoryBatch([FromRoute] Guid batchId, [FromBody] UpdateInventoryDto dto, CancellationToken ct)
        {
            var result = await _batchService.UpdateInventoryAsync(batchId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("{batchId}/harvest")]
        public async Task<IActionResult> HarvestBatch([FromRoute] Guid batchId, [FromBody] UpdateBatchHarvestDateDto dto, CancellationToken ct)
        {
            var result = await _batchService.UpdateHarvestDateAsync(batchId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("{batchId}/sell")]
        public async Task<IActionResult> SellBatch([FromRoute] Guid batchId, [FromBody] SellProductBatchRequestDto dto, CancellationToken ct)
        {
            var result = await _batchService.SellBatchAsync(batchId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
