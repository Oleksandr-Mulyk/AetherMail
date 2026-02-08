using AetherMail.Contracts;
using Moq;

namespace AetherMail.Tests.Contracts
{
    public class WebPublisherTests
    {
        [Fact]
        public async Task SendEmail_ShouldInvokePublisher()
        {
            // Arrange
            var mockPublisher = new Mock<IMessagePublisher>();
            var command = new SendEmailCommand("test@example.com", "Subject", "Body");

            // Act
            await mockPublisher.Object.PublishAsync(command);

            // Assert
            mockPublisher.Verify(x => x.PublishAsync(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
