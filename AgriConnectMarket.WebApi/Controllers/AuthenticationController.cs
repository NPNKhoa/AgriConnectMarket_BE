using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(AuthService _authService, ForgotPasswordService _forgotService, ICloudinaryAdapter _cloudinaryService) : ControllerBase
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

            var dto = new RegisterDto(request.Username, request.Password, request.Email, request.Phone, request.Fullname, request.IsFarmer, avatarUrl);

            var result = await _authService.RegisterAsync(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, "User registered successfully."));
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify([FromQuery] string token, [FromQuery] string? platform, CancellationToken ct)
        {
            var result = await _authService.VerifyAsync(token, ct);

            if (!result.IsSuccess)
            {
                string webFailedUrl = $"http://localhost:5173/email-verified?error={result.Error}";
                return Redirect(webFailedUrl);
            }

            if (!string.IsNullOrEmpty(platform) && platform.Equals("mobile", StringComparison.OrdinalIgnoreCase))
            {
                return Redirect("agriConnectApp://email-verified");
            }

            var ua = Request.Headers["User-Agent"].ToString();
            if (ua.Contains("Android") || ua.Contains("iPhone") || ua.Contains("iPad"))
            {
                if (!result.IsSuccess)
                {
                    string webFailedUrl = $"agriConnectApp://email-verification-fail";
                    return Redirect(webFailedUrl);
                }

                return Redirect("agriConnectApp://email-verified");
            }

            var webSuccessUrl = "http://localhost:5173/email-verified";
            return Redirect(webSuccessUrl);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(dto, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

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

        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("{accountId}/toggle-ban")]
        public async Task<IActionResult> ChangePassword([FromRoute] Guid accountId, CancellationToken ct)
        {
            var result = await _authService.ToggleAccountBanned(accountId, ct);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, MessageConstant.COMMON_UPDATE_SUCCESS_MESSAGE));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto, CancellationToken ct)
        {
            // Validate input format
            await _forgotService.RequestOtpAsync(dto.Email, ct);
            // Always return 200 OK with generic message
            return Ok(new { message = "If this account exists we have sent instructions to reset the password." });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto, CancellationToken ct)
        {
            var result = await _forgotService.VerifyOtpAsync(dto.Email, dto.Otp, ct);
            return Ok(result.Value); // short-lived token
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto, CancellationToken ct)
        {
            await _forgotService.ResetPasswordAsync(dto.Email, dto.ResetToken, dto.NewPassword, ct);
            return Ok(new { message = "Password changed." });
        }
    }
}
