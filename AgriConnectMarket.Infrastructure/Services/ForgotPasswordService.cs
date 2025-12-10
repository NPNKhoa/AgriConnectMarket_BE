using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.SettingObjects;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Result;
using System.Security.Claims;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ForgotPasswordService(IUnitOfWork _uow, IRandomGenerator _randomGenerator, IEmailService _emailService, IDateTimeProvider _clock, IHashingService _hashing, IJwtService _jwt)
    {
        public async Task<Result<string>> RequestOtpAsync(string emailOrPhone, CancellationToken ct)
        {
            var normalized = emailOrPhone.Trim().ToLowerInvariant();

            var user = await _uow.ProfileRepository.GetByEmailAsync(normalized, true);
            if (user == null)
            {
                /*
                 * IMPORTANT: don't reveal.
                 * Return success immediately to avoid the attackers send the random email and find the email that exist in the system
                 */
                return Result<string>.Success(MessageConstant.REQUEST_OTP_SUCCESS);
            }

            var accountId = user.Account.Id;
            // Invalidate previous OTPs for this purpose (optional)
            await _uow.PasswordOtpRepository.InvalidateAllForUserAsync(accountId, OtpPurposeConst.ResetPassword, ct);

            int expiredTimeInMinute = 6;

            var TTL = new TimeSpan(expiredTimeInMinute);
            var rawOtp = _randomGenerator.GenerateNumeric(expiredTimeInMinute);
            var (hash, salt) = _hashing.Hash(rawOtp);

            var now = _clock.UtcNow;
            var otpEntity = new PasswordOtp(accountId, hash, salt, now, TTL, OtpPurposeConst.ResetPassword);
            await _uow.PasswordOtpRepository.AddAsync(otpEntity, ct);

            // Build email body
            var subject = "Your password reset code";
            var body = $@"<p>Your password reset code is <strong>{rawOtp}</strong>. It expires in {expiredTimeInMinute} minutes.</p>";


            var emailMessage = new EmailMessage()
            {
                To = user.Email,
                Subject = subject,
                Body = body,
                IsHtml = true
            };

            await _emailService.SendEmailAsync(emailMessage);

            return Result<string>.Success(MessageConstant.REQUEST_OTP_SUCCESS);
        }

        public async Task<Result<VerifyOtpResult>> VerifyOtpAsync(string emailOrPhone, string otp, CancellationToken ct = default)
        {
            var normalized = emailOrPhone.Trim().ToLowerInvariant();
            var user = await _uow.ProfileRepository.GetByEmailAsync(normalized, true);
            if (user == null) return Result<VerifyOtpResult>.Success(VerifyOtpResult.Failure());

            var account = user.Account;

            var existing = await _uow.PasswordOtpRepository.GetLatestForUserAsync(account.Id, OtpPurposeConst.ResetPassword, ct);
            if (existing == null) return Result<VerifyOtpResult>.Success(VerifyOtpResult.Failure());

            var now = _clock.UtcNow;
            if (existing.IsExpired(now) || existing.Consumed)
            {
                existing.RegisterAttempt(false);
                await _uow.PasswordOtpRepository.UpdateAsync(existing, ct);
                return Result<VerifyOtpResult>.Success(VerifyOtpResult.Failure());
            }

            var verified = _hashing.Verify(otp, existing.HashedOtp, existing.Salt);
            existing.RegisterAttempt(verified);
            await _uow.PasswordOtpRepository.UpdateAsync(existing, ct);

            if (!verified) return Result<VerifyOtpResult>.Success(VerifyOtpResult.Failure());

            var resetToken = _jwt.GenerateAccessToken(account.Id, account.UserName, account.Role);
            return Result<VerifyOtpResult>.Success(VerifyOtpResult.SuccessResult(resetToken));
        }

        public async Task<Result> ResetPasswordAsync(string emailOrPhone, string resetToken, string newPassword, CancellationToken ct)
        {
            var tokenData = _jwt.ValidateToken(resetToken);

            if (tokenData == null) throw new InvalidOperationException("Invalid token");

            var userIdStr = tokenData.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdStr, out Guid accountId))
            {
                return Result.Fail(MessageConstant.UNKNOWN_ERROR);
            }

            var account = await _uow.AuthenRepository.GetByIdAsync(accountId, ct);

            if (account is null)
                return Result.Fail(MessageConstant.ACCOUNT_NOT_FOUND);

            account.Password = newPassword;

            await _uow.AuthenRepository.UpdateAsync(account, ct);

            await _uow.PasswordOtpRepository.InvalidateAllForUserAsync(account.Id, OtpPurposeConst.ResetPassword, ct);

            return Result.Success();
        }
    }
}
