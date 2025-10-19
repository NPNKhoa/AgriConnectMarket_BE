using AgriConnectMarket.Application.DTOs;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Result;
using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class AuthService(IAuthenRepository _authenRepository)
    {
        // Register command
        public async Task<Result<RegisterResultDto>> RegisterAsync(RegisterDto dto)
        {
            // basic input validation
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return Result<RegisterResultDto>.Fail("Email and password are required.");

            var email = dto.Username.Trim().ToLowerInvariant();

            // Check if user exists
            var existing = await _authenRepository.GetByUsernameAsync(email);
            if (existing != null) return Result<RegisterResultDto>.Fail("Email already registered.");

            // Hash the password
            //var passwordHash = _passwordHasher.Hash(dto.Password); LATER

            var passwordHash = HashPassword(dto.Password);

            // Create domain user
            var user = new Account(email, passwordHash);

            // Persist
            await _authenRepository.AddAsync(user);

            // Map to result DTO
            var result = new RegisterResultDto { UserId = user.Id, Username = user.UserName };
            return Result<RegisterResultDto>.Success(result);
        }

        // HELPER

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
