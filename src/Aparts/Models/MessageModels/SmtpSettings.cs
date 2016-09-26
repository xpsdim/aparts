namespace Aparts.Models.MessageModels
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AdminEmail { get; set; }
    }
}
