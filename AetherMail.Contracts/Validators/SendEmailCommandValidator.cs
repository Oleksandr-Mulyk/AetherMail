using FluentValidation;

namespace AetherMail.Contracts.Validators
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(command => command.To).NotEmpty().EmailAddress();
            RuleFor(command => command.Subject).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Body).NotEmpty();
        }
    }
}
