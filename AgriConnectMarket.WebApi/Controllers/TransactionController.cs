using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController(TransactionService _txService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllTransactions(CancellationToken ct)
        {
            var result = await _txService.GetAllTransactionsAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] Guid transactionId, CancellationToken ct)
        {
            var result = await _txService.GetByIdAsync(transactionId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("{transactionId}/resolve")]
        public async Task<IActionResult> ResolveTransaction([FromRoute] Guid transactionId, CancellationToken ct)
        {
            var result = await _txService.ResolveTransactionAsync(transactionId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
