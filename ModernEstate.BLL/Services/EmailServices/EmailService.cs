using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using ModernEstate.Common.Models.Settings;
using System.Net.Mail;

namespace ModernEstate.BLL.Services.EmailServices
{
  public class EmailService : IEmailService
  {
    private readonly MailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<MailSettings> settings, ILogger<EmailService> logger)
    {
      _settings = settings.Value;
      _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string verifyUrl)
    {
      var fromName = Environment.GetEnvironmentVariable("MailSettings__FromName")
                ?? _settings.FromName;
      var fromEmail = Environment.GetEnvironmentVariable("MailSettings__FromEmail")
                      ?? _settings.FromEmail;
      var password = Environment.GetEnvironmentVariable("MailSettings__Password")
                      ?? _settings.Password;
      var host = Environment.GetEnvironmentVariable("MailSettings__Host")
                      ?? _settings.Host;
      var portEnv = Environment.GetEnvironmentVariable("MailSettings__Port");
      int port = 0;
      if (!string.IsNullOrEmpty(portEnv) && int.TryParse(portEnv, out var p))
        port = p;
      else
        port = _settings.Port;
      string html = "<body \n" +
                    "    style=\"font-family: Arial, sans-serif;\n" +
                    "            background-color: #f4f4f4;\n" +
                    "            margin: 0;\n" +
                    "            padding: 0;\n" +
                    "            -webkit-text-size-adjust: none;\n" +
                    "            -ms-text-size-adjust: none;\">\n" +
                    "    <div class=\"email-container\"\n" +
                    "         style=\"max-width: 600px;\n" +
                    "                margin: auto;\n" +
                    "                background-color: #ffffff;\n" +
                    "                padding: 20px;\n" +
                    "                border-radius: 8px;\n" +
                    "                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">\n" +
                    "        <div class=\"header\"\n" +
                    "             style=\"text-align: center;\n" +
                    "                    padding-bottom: 20px;\">\n" +
                    "            <img src=\"https://firebasestorage.googleapis.com/v0/b/diamond-6401b.appspot.com/o/Logo.png?alt=media&token=13f983ed-b3e1-4bbe-83b2-a47edf62c6a6\"\n" +
                    "                alt=\"Logo\" style=\"max-width: 300px;\">\n" +
                    "        </div>\n" +
                    "        <div class=\"content\"\n" +
                    "              style=\"text-align: center;\n" +
                    "                    color: #333333;\">\n" +
                    "            <h1\n" +
                    "            style=\"font-size: 24px;\n" +
                    "                margin: 0;\n" +
                    "                padding: 0;\"\n" +
                    "            >Verify your email address</h1>\n" +
                    "            <p\n" +
                    "            style=\"font-size: 16px;\n" +
                    "                    line-height: 1.5;\">Welcome to Modern Estate.</p>\n" +
                    "            <p\n" +
                    "            style=\"font-size: 16px;\n" +
                    "                line-height: 1.5;\">Please click the button below to confirm your email address and activate your account.</p>\n" +
                    "            <a href=\"" + verifyUrl + "\" class=\"btn\"\n" +
                    "               style=\"display: inline-block;\n" +
                    "               margin-top: 20px;\n" +
                    "               padding: 15px 25px;\n" +
                    "               font-size: 16px;\n" +
                    "               color: #ffffff;\n" +
                    "               background-color: #f52d56;\n" +
                    "               border-radius: 5px;\n" +
                    "               text-decoration: none;\">Confirm Email</a>\n" +
                    "<p style=\"margin-top: 20px; font-size: 14px; color: #555555;\">" +
                    "If the button doesn't work, please press the link below:\n" +
                    "</p>\n" +
                    "<p style=\"font-size: 14px; color: #1a0dab;\">\n" +
                    "    <a href=\"" + verifyUrl + "\" style=\"color: #1a0dab; text-decoration: underline;\">Verify your account</a>\n" +
                    "</p>" +
                    "            <p>If you received this email in error, simply ignore this email and do not click the button.</p>\n" +
                    "        </div>\n" +
                    "        <div class=\"footer\"\n" +
                    "             style=\"text-align: center;\n" +
                    "             font-size: 14px;\n" +
                    "             color: #777777;\n" +
                    "             margin-top: 20px;\">\n" +
                    "            <h2>Thank you, have a good day .</h2></br>Modern Estate Team\n" +
                    "        </div>\n" +
                    "    </div>\n" +
                    "</body>";
      var email = new MimeMessage();
      email.From.Add(new MailboxAddress(fromName, fromEmail));
      email.To.Add(MailboxAddress.Parse(to));
      email.Subject = subject;

      var builder = new BodyBuilder
      {
        HtmlBody = html
      };
      email.Body = builder.ToMessageBody();

      using var smtp = new MailKit.Net.Smtp.SmtpClient();
      await smtp.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
      await smtp.AuthenticateAsync(fromEmail, password);
      await smtp.SendAsync(email);
      await smtp.DisconnectAsync(true);

      _logger.LogInformation("Sent email to {Email}", to);
    }

        public async Task SendEmailResetPasswordAsync(string to, string subject, string OTP)
        {
            string html = @"
<div style=""max-width:600px;margin:auto;background-color:#ffffff;padding:30px;border-radius:8px;box-shadow:0 2px 10px rgba(0,0,0,0.1);font-family:Arial,sans-serif;"">
  <div style=""text-align:center;margin-bottom:30px;"">
    <h1 style=""color:#2c3e50;margin:0;"">Modern Estate</h1>
  </div>
  <div style=""color:#333;line-height:1.6;"">
    <p>Hello,</p>
    <p>Your One-Time Password (OTP) for resetting your password is:</p>
    <div style=""text-align:center;margin:20px 0;"">
      <span style=""display:inline-block;font-size:28px;letter-spacing:8px;background:#f1f1f1;padding:15px 25px;border-radius:8px;font-weight:bold;color:#2c3e50;"">
        "+ OTP + @"
      </span>
    </div>
    <p>This code will expire in 5 minutes. If you didn’t request this, please ignore this email.</p>
    <p>Best regards,<br/>The Modern Estate Team</p>
  </div>
  <div style=""font-size:12px;color:#888;text-align:center;margin-top:40px;"">
    &copy; Modern Estate. All rights reserved.
  </div>
</div>
";
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = html
            };
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.FromEmail, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Sent email to {Email}", to);
        }
    }
}
