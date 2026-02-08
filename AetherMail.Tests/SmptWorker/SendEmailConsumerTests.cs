using AetherMail.Contracts;
using AetherMail.SmtpWorker.Consumers;
using FluentAssertions;
using MassTransit.Testing;
using Microsoft.Extensions.Logging;
using Moq;

namespace AetherMail.Tests.SmptWorker
{
    public class SendEmailConsumerTests
    {
        [Fact]
        public async Task Consume_Should_Process_Command()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SendEmailConsumer>>();

            using var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer(() => new SendEmailConsumer(loggerMock.Object));

            await harness.Start();
            try
            {
                var command = new SendEmailCommand("test@example.com", "Subject", "Body");

                // Act
                await harness.InputQueueSendEndpoint.Send(command);

                // Assert
                (await harness.Consumed.Any<SendEmailCommand>()).Should().BeTrue();
                (await consumerHarness.Consumed.Any<SendEmailCommand>()).Should().BeTrue();
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
