using AetherMail.Contracts;
using MassTransit;

namespace AetherMail.SmtpWorker.Consumers
{
    public class SendEmailConsumer(ILogger<SendEmailConsumer> logger) : IConsumer<SendEmailCommand>
    {
        public async Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            var command = context.Message;

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation
                    ("Received SendEmailCommand: To={To}, Subject={Subject}", command.To, command.Subject);
            }

            await Task.CompletedTask;
        }
    }
}