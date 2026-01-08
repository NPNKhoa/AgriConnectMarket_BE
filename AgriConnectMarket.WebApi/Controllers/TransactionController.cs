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
    }
}
