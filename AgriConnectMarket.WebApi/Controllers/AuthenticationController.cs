using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(AuthService _authService, ICloudinaryAdapter _cloudinaryService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] Models.RegisterRequest request, CancellationToken ct)
        {
            string avatarUrl = string.Empty;

            // Upload avatar to Cloudinary
            if (request.Avatar is not null)
            {
                var uploadResult = await _cloudinaryService.UploadAsync(request.Avatar, ct);

                if (!uploadResult.Success)
                {
                    return BadRequest(ApiResponse.FailResponse(uploadResult.Error!));
                }

                avatarUrl = uploadResult.Url ?? string.Empty;
            }

            var dto = new RegisterDto(request.Username, request.Password, request.Email, request.Phone, request.Fullname, avatarUrl);

            var result = await _authService.RegisterAsync(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, "User registered successfully."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            Console.WriteLine(result.Value.ToString());

            return Ok(ApiResponse<LoginResultDto>.SuccessResponse(result.Value, MessageConstant.LOGIN_SUCCESS));
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto, CancellationToken ct)
        {
            var result = await _authService.ChangePasswordAsync(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpPatch("me/deactive")]
        public async Task<IActionResult> DeactiveMyAccount(CancellationToken ct)
        {
            var result = await _authService.DeactiveAccount(ct);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse.FailResponse(result.Error));
            }

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.DEACTIVE_ACCOUNT_SUCCESS));
        }
    }
}
