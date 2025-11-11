using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Result;
using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class AuthService(IUnitOfWork _uow, IJwtService _jwtService, ICurrentUserService _currentUserService)
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
            var existing = await _uow.AuthenRepository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return Result<RegisterResultDto>.Fail(MessageConstant.EXISTING_USERNAME);

            // Hash the password
            var passwordHash = HashPassword(dto.Password);

            // Create domain user
            var user = new Account(dto.Username, passwordHash, dto.IsFarmer);
            var profile = new Profile(dto.Fullname, dto.Email, dto.Phone, user.Id, dto.AvatarUrl)
            {
                Account = user
            };

            await _uow.ProfileRepository.AddAsync(profile);
            await _uow.SaveChangesAsync();


            // Map to result DTO
            var result = new RegisterResultDto { UserId = user.Id, Username = user.UserName };
            return Result<RegisterResultDto>.Success(result);
        }

        public async Task<Result<LoginResultDto>> LoginAsync(LoginDto dto, CancellationToken ct = default)
        {
            // basic input validation
            Guard.AgainstNullOrWhiteSpace(dto.Username, nameof(dto.Username));
            Guard.AgainstNullOrWhiteSpace(dto.Password, nameof(dto.Password));

            // Check if user exists
            var existing = await _uow.AuthenRepository.GetByUsernameAsync(dto.Username, true);

            if (existing == null)
            {
                return Result<LoginResultDto>.Fail(MessageConstant.WRONG_CREDENTIALS);
            }

            if (!VerifyPassword(dto.Password, existing.Password))
            {
                return Result<LoginResultDto>.Fail(MessageConstant.WRONG_CREDENTIALS);
            }

            var user = await _uow.ProfileRepository.GetByAccountIdAsync(existing.Id);

            if (user is null)
            {
                return Result<LoginResultDto>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            var token = _jwtService.GenerateAccessToken(user.Id, existing.UserName, existing.Role); // user.Id is the ProfileID

            var result = new LoginResultDto { AccountId = existing.Id, UserId = user.Id, Token = token };

            return Result<LoginResultDto>.Success(result);
        }

        public async Task<Result<ChangePasswordResultDto>> ChangePasswordAsync(ChangePasswordDto dto, CancellationToken ct = default)
        {
            var existing = await _uow.ProfileRepository.GetByEmailAsync(dto.Email, true);

            if (existing is null)
            {
                return Result<ChangePasswordResultDto>.Fail(MessageConstant.EMAIL_NOT_FOUND);
            }

            if (!VerifyPassword(dto.OldPassword, existing.Account.Password))
            {
                return Result<ChangePasswordResultDto>.Fail(MessageConstant.WRONG_CREDENTIALS);
            }

            var account = await _uow.AuthenRepository.GetByIdAsync(existing.AccountId);

            if (account is null)
            {
                return Result<ChangePasswordResultDto>.Fail(MessageConstant.WRONG_CREDENTIALS);
            }

            account.Password = HashPassword(dto.NewPassword);
            await _uow.SaveChangesAsync();

            var result = new ChangePasswordResultDto()
            {
                UserId = existing.Id
            };


            return Result<ChangePasswordResultDto>.Success(result);
        }

        public async Task<Result<Guid>> DeactiveAccount(CancellationToken ct)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<Guid>.Fail(MessageConstant.NOTE_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var user = await _uow.ProfileRepository.GetByIdAsync(userId);

            if (user is null)
            {
                return Result<Guid>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            var account = await _uow.AuthenRepository.GetByIdAsync(user.AccountId);

            if (account is null)
            {
                return Result<Guid>.Fail(MessageConstant.ACCOUNT_NOT_FOUND);
            }

            account.IsActive = false;
            await _uow.AuthenRepository.UpdateAsync(account, ct);

            return Result<Guid>.Success(userId);
        }

        public async Task<Result<Account>> ToggleAccountBanned(Guid accountId, CancellationToken ct = default)
        {

            var account = await _uow.AuthenRepository.GetByIdAsync(accountId, ct);

            if (account is null)
            {
                return Result<Account>.Fail(MessageConstant.ACCOUNT_NOT_FOUND);
            }

            account.ToggleAccountBannedStatus();

            await _uow.SaveChangesAsync(ct);

            return Result<Account>.Success(account);
        }

        // ============ HELPERS ============

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string inputPassword, string storedHash)
        {
            using var sha = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(inputPassword);
            var inputHashBytes = sha.ComputeHash(inputBytes);
            var inputHash = Convert.ToBase64String(inputHashBytes);

            return inputHash == storedHash;
        }

    }
}
