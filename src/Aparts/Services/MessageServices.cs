using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Aparts.Models.MessageModels;
using Microsoft.Extensions.Options;

namespace Aparts.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly SmtpSettings _smtpSettings;

        public AuthMessageSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(_smtpSettings.FromEmail, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password)
            };
            return smtpClient.SendMailAsync(mailMessage);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
