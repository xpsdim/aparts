using System.Threading.Tasks;

namespace Aparts.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
