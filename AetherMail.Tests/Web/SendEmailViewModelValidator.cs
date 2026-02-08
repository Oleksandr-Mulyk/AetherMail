using AetherMail.Web.ViewModels;
using AetherMail.Web.ViewModels.Validators;
using FluentAssertions;

namespace AetherMail.Tests.Web
{
    public class SendEmailViewModelValidatorTests
    {
        private readonly SendEmailViewModelValidator _validator = new();

        [Fact]
        public async Task ValidateValue_Should_Forward_Error_From_CommandValidator()
        {
            var model = new SendEmailViewModel { To = "invalid-email" };

            // Act
            var result = await _validator.ValidateValue(model, nameof(model.To));

            // Assert
            result.Should().NotBeEmpty();
            result.First().Should().Contain("email");
        }

        [Fact]
        public async Task ValidateValue_Should_Return_Empty_When_Data_Is_Valid()
        {
            // Arrange
            var model = new SendEmailViewModel
            {
                To = "test@example.com",
                Subject = "Test",
                Body = "Test"
            };

            // Act
            var result = await _validator.ValidateValue(model, nameof(model.To));

            // Assert
            result.Should().BeEmpty();
        }
    }
}