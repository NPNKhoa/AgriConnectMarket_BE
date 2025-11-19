using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController(ProfileService _profileService) : ControllerBase
    {
        [HttpPut("{profileId}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] Guid profileId, UpdateProfileDto dto)
        {
            var result = await _profileService.UpdateProfile(profileId, dto);


            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCustomerList([FromQuery] string? searchTerm, CancellationToken ct)
        {
            var result = await _profileService.GetFullListAsync(ct);

            if (searchTerm is not null)
            {
                result = await _profileService.GetProfilesAsync(searchTerm, ct);
            }

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetProfileById([FromRoute] Guid profileId, CancellationToken ct)
        {
            var result = await _profileService.GetProfileById(profileId, ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile(CancellationToken ct)
        {
            var result = await _profileService.GetMyProfile(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_RETRIVE_SUCCESS_MESSAGE));
        }
    }
}
