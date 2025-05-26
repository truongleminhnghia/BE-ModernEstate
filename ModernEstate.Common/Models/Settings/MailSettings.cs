

namespace ModernEstate.Common.Models.Settings
{
    public class MailSettings
    {
        public string FromEmail { get; set; } = default!;
        public string FromName { get; set; } = "Modern Estate";
        public string Password { get; set; } = default!;
        public string Host { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
    }

}
