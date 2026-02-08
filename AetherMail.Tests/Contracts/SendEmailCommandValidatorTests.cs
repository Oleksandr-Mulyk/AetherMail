using AetherMail.Contracts;
using AetherMail.Contracts.Validators;
using FluentValidation.TestHelper;

namespace AetherMail.Tests.Contracts
{
    public class SendEmailCommandValidatorTests
    {
        private readonly SendEmailCommandValidator _validator = new();

        [Theory]
        [InlineData("")]
        [InlineData("not-an-email")]
        public void Should_Have_Error_When_Email_Is_Invalid(string invalidEmail)
        {
            var model = new SendEmailCommand(invalidEmail, "Subject", "Body");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.To);
        }

        [Fact]
        public void Should_Have_Error_When_Subject_Is_Empty()
        {
            var model = new SendEmailCommand("test@example.com", "", "Body");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Subject);
        }

        [Fact]
        public void Should_Have_Error_When_Subject_Exceeds_200_Characters()
        {
            // Arrange
            var longSubject = new string('a', 201);
            var model = new SendEmailCommand("test@example.com", longSubject, "Body");

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Subject);
        }

        [Fact]
        public void Should_Have_Error_When_Body_Is_Empty()
        {
            var model = new SendEmailCommand("test@example.com", "Subject", "");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Body);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Model_Is_Valid()
        {
            var model = new SendEmailCommand("test@examle.com", "Correct Subject", "Correct Body");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
