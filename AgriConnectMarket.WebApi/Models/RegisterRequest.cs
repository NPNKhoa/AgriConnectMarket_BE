using System.ComponentModel.DataAnnotations;

namespace AgriConnectMarket.WebApi.Models
{
    public class RegisterRequest
    {
        [Required] public string Username { get; set; } = default!;
        [Required] public string Email { get; set; } = default!;
        [Required] public string Password { get; set; } = default!;
        [Required] public string Fullname { get; set; } = default!;
        [Required] public string Phone { get; set; } = default!;
        public bool IsFarmer { get; set; } = false;
        public IFormFile? Avatar { get; set; }
    }
}
