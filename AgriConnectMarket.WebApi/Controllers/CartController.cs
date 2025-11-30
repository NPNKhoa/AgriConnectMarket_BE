using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController(CartService _cartService) : ControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetCartInfo(CancellationToken ct)
        {
            var result = await _cartService.GetCartByUser(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPost("")]
        public async Task<IActionResult> AddToCart([FromBody] CreateCartItemDto dto, CancellationToken ct)
        {
            var result = await _cartService.AddToCartAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPatch("{cartId}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] Guid cartId, [FromBody] UpdateCartItemDto dto, CancellationToken ct)
        {
            var result = await _cartService.UpdateCartItemAsync(cartId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpDelete("cart-items/{itemId}")]
        public async Task<IActionResult> AddToCart([FromRoute] Guid itemId, CancellationToken ct)
        {
            var result = await _cartService.DeleteItemFromCartAsync(itemId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
