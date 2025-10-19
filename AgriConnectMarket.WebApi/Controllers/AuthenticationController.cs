using AgriConnectMarket.Application.DTOs;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(AuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse.FailResponse(result.Error));

            return Ok(ApiResponse.SuccessResponse(result.Value, "User registered successfully."));
        }
    }
}
