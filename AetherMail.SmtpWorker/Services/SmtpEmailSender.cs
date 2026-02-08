using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace AetherMail.SmtpWorker.Services
{
    public class SmtpEmailSender(IConfiguration config, ILogger<SmtpEmailSender> logger) : IEmailSender
    {
        public async Task SendAsync(string to, string subject, string body, CancellationToken ct = default)
        {
            var connectionString = config.GetConnectionString("mailserver");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Mailserver connection string is missing.");

            var parts = connectionString.Split(':');
            var host = parts[0];
            var port = parts.Length > 1 ? int.Parse(parts[1]) : 1025;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("AetherMail", "noreply@aether.local"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();

            await client.ConnectAsync(host, port, SecureSocketOptions.None, ct);
            await client.SendAsync(message, ct);
            await client.DisconnectAsync(true, ct);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Email sent to {To} via {Host}:{Port}", to, host, port);
            }
        }
    }
}
