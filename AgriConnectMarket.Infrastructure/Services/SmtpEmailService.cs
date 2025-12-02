using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.SettingObject;
using AgriConnectMarket.Application.SettingObjects;
using MailKit.Net.Smtp;
using MimeKit;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class SmtpEmailService(SmtpSettings _smtpSettings) : IEmailService
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpSettings.From));
            email.To.Add(MailboxAddress.Parse(message.To));
            email.Subject = message.Subject;

            if (message.IsHtml)
            {
                email.Body = new BodyBuilder { HtmlBody = message.Body }.ToMessageBody();
            }
            else
            {
                email.Body = new TextPart("plain") { Text = message.Body };
            }

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, _smtpSettings.UseSsl);
            await smtp.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
