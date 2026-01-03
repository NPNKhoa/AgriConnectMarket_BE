using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController(OrderService _orderService, VnPayService _vnPayService) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, CancellationToken ct)
        {
            var result = await _orderService.CreateOrder(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPost("pre-order")]
        public async Task<IActionResult> CreatePreOrder([FromBody] CreatePreOrderDto dto, CancellationToken ct)
        {
            var result = await _orderService.CreatePreOrder(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
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

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetail([FromRoute] Guid orderId, CancellationToken ct)
        {
            var result = await _orderService.GetOrderDetailAsync(orderId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("order-code/{orderCode}")]
        public async Task<IActionResult> GetOrderDetail([FromRoute] string orderCode, CancellationToken ct)
        {
            var result = await _orderService.GetOrderByOrderCodeAsync(orderCode, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("pre-orders/{orderId}")]
        public async Task<IActionResult> GetPreOrderDetail([FromRoute] Guid orderId, CancellationToken ct)
        {
            var result = await _orderService.GetPreOrderDetailAsync(orderId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("pre-orders/order-code/{orderCode}")]
        public async Task<IActionResult> GetPreOrderDetail([FromRoute] string orderCode, CancellationToken ct)
        {
            var result = await _orderService.GetOrderByOrderCodeAsync(orderCode, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("farm/{farmId}")]
        public async Task<IActionResult> GetFarmOrders([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _orderService.GetFarmOrderAsync(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("/api/farm/{farmId}/pre-orders")]
        public async Task<IActionResult> GetFarmPreOrders([FromRoute] Guid farmId, CancellationToken ct)
        {
            var result = await _orderService.GetFarmPreOrderAsync(farmId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyOrders(CancellationToken ct)
        {
            var result = await _orderService.GetMyOrdersAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("pre-orders/me")]
        public async Task<IActionResult> GetMyPreOrders(CancellationToken ct)
        {
            var result = await _orderService.GetMyOrdersAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("pre-orders/{orderId}/approve")]
        public async Task<IActionResult> ApprovePreOrders([FromRoute] Guid orderId, [FromBody] ApprovePreOrder dto, CancellationToken ct)
        {
            var result = await _orderService.ApprovePreOrder(orderId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }


        [HttpPatch("{orderId}/order-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid orderId, [FromBody] UpdateOrderStatusDto dto, CancellationToken ct)
        {
            var result = await _orderService.UpdateOrderStatus(orderId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("{orderId}/process")]
        public async Task<IActionResult> ProcessOrderNextStep([FromRoute] Guid orderId, CancellationToken ct)
        {
            var result = await _orderService.ProcessOrder(orderId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId, CancellationToken ct)
        {
            var result = await _orderService.CancelOrder(orderId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
