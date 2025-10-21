using AgriConnectMarket.Application.DTOs;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Result;
using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class AuthService(IAuthenRepository _authenRepository, AppDbContext _context)
    {
        // Register command
        public async Task<Result<RegisterResultDto>> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {
            // basic input validation
            Guard.AgainstNullOrWhiteSpace(dto.Username, nameof(dto.Username));
            Guard.AgainstNullOrWhiteSpace(dto.Password, nameof(dto.Password));
            Guard.AgainstNullOrWhiteSpace(dto.Email, nameof(dto.Email));
            Guard.AgainstNullOrWhiteSpace(dto.Fullname, nameof(dto.Fullname));
            Guard.AgainstNullOrWhiteSpace(dto.Phone, nameof(dto.Phone));

            // Check if user exists
            var existing = await _authenRepository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return Result<RegisterResultDto>.Fail(MessageConstant.EXISTING_USERNAME);

            // Hash the password
            //var passwordHash = _passwordHasher.Hash(dto.Password); LATER

            var passwordHash = HashPassword(dto.Password);



            // Create domain user
            var user = new Account(dto.Email, passwordHash);

            // Persist
            await _authenRepository.AddAsync(user);
            var profile = new Profile("dto.Fullname", "dto.Username", "dto.Phone", user.Id, dto.AvatarUrl!);
            //await _authenRepository.AddAsync(profile);
            await _context.Set<Profile>().AddAsync(profile);
            await _context.SaveChangesAsync();

            // Map to result DTO
            var result = new RegisterResultDto { UserId = user.Id, Username = user.UserName };
            return Result<RegisterResultDto>.Success(result);
        }

        // ============ HELPERS ============

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
