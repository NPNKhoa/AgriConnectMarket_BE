using AgriConnectMarket.Application.DTOs;
using AgriConnectMarket.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectMarket.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(IAccount _account) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAllPetTypes([FromBody] RegisterDTO dto)
        {
            try
            {
                await _account.CreateAsync(new Domain.Entities.Account()
                {
                    UserName = dto.Username,
                    Password = dto.Password,
                    IsActive = true,
                    IsDeLeted = false,
                    Role = "CUSTOMER"
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error, {ex}");
            }
        }

    }
}
