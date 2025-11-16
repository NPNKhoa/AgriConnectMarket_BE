using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(ProductService _productService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryRequest? query, CancellationToken ct)
        {
            dynamic result = await _productService.GetAllProductsAsync(ct);

            if (query is not null)
            {
                result = await _productService.GetProductsAsync(query, ct);
            }

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid productId, CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(productId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto, CancellationToken ct)
        {
            var result = await _productService.CreateProductAsync(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId, [FromBody] UpdateProductDto dto, CancellationToken ct)
        {
            var result = await _productService.UpdateProductAsync(productId, dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId, CancellationToken ct)
        {
            var result = await _productService.DeleteProductAsync(productId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_DELETE_SUCCESS_MESSAGE));
        }
    }
}
