using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController(AddressService _addressService) : ControllerBase
    {
        [HttpGet("")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetALlAddresses(CancellationToken ct)
        {
            var result = await _addressService.GetAllAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyAddresses(CancellationToken ct)
        {
            var result = await _addressService.GetUserAddressesAsync(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto dto, CancellationToken ct)
        {
            var result = await _addressService.CreateAddessAsync(dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_CREATE_SUCCESS_MESSAGE));
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] Guid addressId, [FromBody] UpdateAddressDto dto, CancellationToken ct)
        {
            var result = await _addressService.UpdateAddressAsync(addressId, dto, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid addressId, CancellationToken ct)
        {
            var result = await _addressService.DeleteAddressAsync(addressId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_DELETE_SUCCESS_MESSAGE));
        }
    }
}
