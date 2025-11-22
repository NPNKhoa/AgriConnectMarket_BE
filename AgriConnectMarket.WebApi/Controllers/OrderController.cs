using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController(OrderService _orderService) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, CancellationToken ct)
        {
            var result = await _orderService.CreateOrder(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllOrders(CancellationToken ct)
        {
            var result = await _orderService.GetAllOrdersAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result));
        }

        [HttpGet("farm/{farmId}")]
        public async Task<IActionResult> GetFarmOrders([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _orderService.GetFarmOrderAsync(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyOrders(CancellationToken ct)
        {
            var result = await _orderService.GetMyOrdersAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result));
        }

        [HttpPatch("{orderId}/order-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid orderId, [FromBody] UpdateOrderStatusDto dto, CancellationToken ct)
        {
            var result = await _orderService.UpdateOrderStatus(orderId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result));
        }

        [HttpPatch("{orderId}/process")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId, CancellationToken ct)
        {
            var result = await _orderService.ProcessOrder(orderId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result));
        }

        [HttpPatch("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId, CancellationToken ct)
        {
            var result = await _orderService.CancelOrder(orderId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result));
        }
    }
}
