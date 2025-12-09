using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;

namespace AgriConnectMarket.Domain.Entities
{
    public class PasswordOtp : BaseEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public string HashedOtp { get; private set; }       // hashed OTP (e.g., using SHA-256 or IPasswordHasher)
        public string Salt { get; private set; }            // optional per-row salt if you use SHA
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset ExpiresAt { get; private set; }
        public bool Consumed { get; private set; }
        public int AttemptCount { get; private set; }
        public int MaxAttempts { get; private set; }
        public string Purpose { get; private set; }  // enum e.g. ResetPassword, VerifyEmail

        public PasswordOtp() { } // EF
        public PasswordOtp(Guid userId, string hashedOtp, string salt, DateTimeOffset now, TimeSpan ttl, string purpose, int maxAttempts = 5)
        {
            Guard.AgainstInvalidEnumValue(typeof(OtpPurposeConst), purpose, nameof(purpose));

            UserId = userId;
            HashedOtp = hashedOtp;
            Salt = salt;
            CreatedAt = now;
            ExpiresAt = now.Add(ttl);
            Purpose = purpose;
            MaxAttempts = maxAttempts;
            Consumed = false;
        }

        public bool IsExpired(DateTimeOffset now) => now >= ExpiresAt;
        public void RegisterAttempt(bool success)
        {
            AttemptCount++;
            if (success) Consumed = true;
            if (AttemptCount >= MaxAttempts && !success) Consumed = true; // lock out
        }
    }

}
