//using AgriConnectMarket.Application.DTOs.RequestDtos;
//using AgriConnectMarket.Application.Interfaces;
//using AgriConnectMarket.Infrastructure.Services;
//using AgriConnectMarket.SharedKernel.Responses;
//using Microsoft.AspNetCore.Mvc;

//namespace AgriConnectMarket.WebApi.Controllers
//{
//    [Route("api/preorders")]
//    [ApiController]
//    public class PreOrderController(PreOrderService _preOrderService, ICurrentUserService _currentUserService) : ControllerBase
//    {
//        [HttpPost("")]
//        public async Task<IActionResult> CreatePreOrder([FromBody] CreatePreOrderDto dto, CancellationToken ct)
//        {
//            var userId = _currentUserService.UserId;
//            if (userId is null) return Unauthorized();

//            var result = await _preOrderService.CreatePreOrderAsync(userId.Value, dto.FarmId, dto.ProductId, dto.Quantity, dto.Note, ct);

//            if (!result.IsSuccess)
//                return BadRequest(ApiResponse.FailResponse(result.Error));

//            return Ok(ApiResponse.SuccessResponse(result.Value));
//        }

//        [HttpGet("me")]
//        public async Task<IActionResult> GetMyPreOrders(CancellationToken ct)
//        {
//            var userId = _currentUserService.UserId;
//            if (userId is null) return Unauthorized();

//            var result = await _preOrderService.GetCustomerPreOrdersAsync(userId.Value, ct);

//            if (!result.IsSuccess)
//                return BadRequest(ApiResponse.FailResponse(result.Error));

//            return Ok(ApiResponse.SuccessResponse(result.Value));
//        }

//        [HttpGet("farm/{farmId}")]
//        public async Task<IActionResult> GetFarmPreOrders([FromRoute] Guid farmId, CancellationToken ct)
//        {
//            // In a real app, we should check if the current user is the owner of the farm
//            var result = await _preOrderService.GetFarmerPreOrdersAsync(farmId, ct);

//            if (!result.IsSuccess)
//                return BadRequest(ApiResponse.FailResponse(result.Error));

//            return Ok(ApiResponse.SuccessResponse(result.Value));
//        }

//        [HttpPut("{preOrderId}/date")]
//        public async Task<IActionResult> UpdateReleaseDate([FromRoute] Guid preOrderId, [FromBody] UpdatePreOrderDateDto dto, CancellationToken ct)
//        {
//            var result = await _preOrderService.UpdatePreOrderDateAsync(preOrderId, dto.ExpectedReleaseDate, ct);

//            if (!result.IsSuccess)
//                return BadRequest(ApiResponse.FailResponse(result.Error));

//            return Ok(ApiResponse.SuccessResponse(result.Value));
//        }

//        [HttpGet("suggestions/farm/{farmId}")]
//        public async Task<IActionResult> GetProductSuggestions([FromRoute] Guid farmId, CancellationToken ct)
//        {
//            var result = await _preOrderService.GetFarmProductSuggestionsAsync(farmId, ct);

//            if (!result.IsSuccess)
//                return BadRequest(ApiResponse.FailResponse(result.Error));

//            return Ok(ApiResponse.SuccessResponse(result.Value));
//        }
//    }
//}
