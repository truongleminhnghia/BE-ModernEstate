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
      string html = $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8'>
  <title>Email Verification</title>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 40px;'>
  <table width='100%' style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 8px rgba(0,0,0,0.1);'>
    <tr>
      <td style='text-align: center;'>
        <h2 style='color: #333;'>Welcome to Modern Estate</h2>
        <p style='font-size: 16px; color: #555;'>Please confirm your email address by clicking the button below:</p>
        <a href='{verifyUrl}' style='display: inline-block; margin-top: 20px; padding: 12px 25px; font-size: 16px; color: white; background-color: #28a745; text-decoration: none; border-radius: 5px;'>Verify Email</a>
        <p style='font-size: 14px; color: #999; margin-top: 30px;'>If you did not request this, you can safely ignore this email.</p>
      </td>
    </tr>
  </table>
</body>
</html>";
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
  }
}
