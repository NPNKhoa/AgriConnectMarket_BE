namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class VerifyOtpResult
    {
        public bool Success { get; private set; }
        public string? ResetToken { get; private set; }

        private VerifyOtpResult(bool success, string? resetToken)
        {
            Success = success;
            ResetToken = resetToken;
        }

        public static VerifyOtpResult Failure()
            => new VerifyOtpResult(false, null);

        public static VerifyOtpResult SuccessResult(string resetToken)
            => new VerifyOtpResult(true, resetToken);
    }

}
