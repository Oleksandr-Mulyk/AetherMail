using AetherMail.Contracts;
using FluentAssertions;

namespace AetherMail.Tests
{
    public class ContractTests
    {
        [Fact]
        public void SendEmailCommand_ShouldRetainProperties()
        {
            // Arrange & Act
            var command = new SendEmailCommand("test@mail.com", "Hello", "Body");

            // Assert
            command.To.Should().Be("test@mail.com");
            command.Subject.Should().Be("Hello");
        }
    }
}
