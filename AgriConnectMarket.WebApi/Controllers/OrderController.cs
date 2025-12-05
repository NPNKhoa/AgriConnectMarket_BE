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

            //if (dto.PaymentMethod is not null && dto.PaymentMethod.Contains(PaymentMethodConst.ONLINE))
            //{
            //    var clientIp = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString() ?? "127.0.0.1";

            //    if (result.Value is null)
            //    {
            //        return BadRequest(ApiResponse.FailResponse(MessageConstant.ORDER_DATA_NOT_FOUND));
            //    }

            //    var vnPayResult = await _vnPayService.CreatePaymentUrlAsync(result.Value.OrderId, clientIp, ct);

            //    if (!vnPayResult.IsSuccess)
            //    {
            //        return BadRequest(ApiResponse.FailResponse(MessageConstant.CREATE_PAYMENT_URL_FAIL));
            //    }

            //    if (result.Value is null)
            //    {
            //        return BadRequest(ApiResponse.FailResponse(MessageConstant.UNKNOWN_ERROR));
            //    }

            //    Redirect(vnPayResult.Value!.PaymentUrl);
            //}

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
