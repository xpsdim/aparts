using System.Threading.Tasks;
using Aparts.Models.MessageModels;

namespace Aparts.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        SmtpSettings SmtpSettings();
    }
}
