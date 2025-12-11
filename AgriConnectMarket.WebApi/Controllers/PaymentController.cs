using AgriConnectMarket.Infrastructure.Repositories;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController(VnPayService _VNPayService) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateUrl([FromBody] CreatePaymentRequestDto dto, CancellationToken ct)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString() ?? "127.0.0.1";
            var result = await _VNPayService.CreatePaymentUrlAsync(dto.OrderId, clientIp, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(result.Value);
        }

        [HttpGet("vnpay-return")]
        public async Task<IActionResult> VnPayReturn()
        {
            // Querystring contains vnp_* parameters
            var result = await _VNPayService.HandleReturnAsync(Request.Query);

            return Redirect($"http://localhost:5173/payment-result?responseCode={result.Value!.responseCode}&orderCode={result.Value.orderCode}");
        }

        [HttpGet("vnpay-ipn")]
        public async Task<IActionResult> VnPayIpn()
        {
            var ok = await _VNPayService.HandleIpnAsync(Request.Query);
            if (ok)
            {
                // VNPay expects response body "OK"
                return Content("OK");
            }
            return BadRequest("Invalid");
        }
    }
}
