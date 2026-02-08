using AetherMail.Contracts;
using AetherMail.SmtpWorker.Services;
using MassTransit;

namespace AetherMail.SmtpWorker.Consumers
{
    public class SendEmailConsumer(IEmailSender emailSender) : IConsumer<SendEmailCommand>
    {
        public async Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            var msg = context.Message;
            await emailSender.SendAsync(msg.To, msg.Subject, msg.Body, context.CancellationToken);
        }
    }
}