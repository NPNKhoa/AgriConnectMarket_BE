using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using AgriConnectMarket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(CategoryService _service, ICloudinaryAdapter _cloudinaryService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllCategory(CancellationToken ct)
        {
            var result = await _service.GetAllCategoryAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequest request, CancellationToken ct)
        {
            string imageUrl = string.Empty;

            if (request.IllustractiveImage is not null)
            {
                var uploadResult = await _cloudinaryService.UploadAsync(request.IllustractiveImage, ct);

                if (!uploadResult.Success)
                {
                    return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                }

                imageUrl = uploadResult.Url ?? string.Empty;
            }

            var dto = new CreateCategoryDto()
            {
                CategortName = request.CategortName,
                CategoryDesc = request.CategoryDesc,
                IllustractiveImageUrl = imageUrl
            };

            var result = await _service.CreateCategory(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromForm] UpdateCategoryRequest request, CancellationToken ct)
        {
            string imageUrl = string.Empty;

            if (request.IllustractiveImage is not null)
            {
                var uploadResult = await _cloudinaryService.UploadAsync(request.IllustractiveImage, ct);

                if (!uploadResult.Success)
                {
                    return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                }

                imageUrl = uploadResult.Url ?? string.Empty;
            }

            var dto = new UpdateCategoryDto()
            {
                CategortName = request.CategortName,
                CategoryDesc = request.CategoryDesc,
                IllustractiveImageUrl = imageUrl
            };

            var result = await _service.UpdateCategoryAsync(categoryId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId, CancellationToken ct)
        {
            var result = await _service.DeleteCategoryAsync(categoryId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value));
        }
    }
}
