using AgriConnectMarket.Application.SettingObjects;


namespace AgriConnectMarket.Application.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailMessage emailMessage);
    }
}
