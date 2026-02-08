using AetherMail.Contracts;
using AetherMail.Contracts.Validators;
using FluentValidation;

namespace AetherMail.Web.ViewModels.Validators
{
    public class SendEmailViewModelValidator : AbstractValidator<SendEmailViewModel>
    {
        private readonly SendEmailCommandValidator _commandValidator = new();

        public SendEmailViewModelValidator()
        {
            RuleFor(viewModel => viewModel).CustomAsync(
                async (model, context, cancellation) =>
                {
                    var command = new SendEmailCommand(model.To, model.Subject, model.Body);
                    var result = await _commandValidator.ValidateAsync(command, cancellation);

                    foreach (var error in result.Errors)
                    {
                        context.AddFailure(error.PropertyName, error.ErrorMessage);
                    }
                });
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            if (model is not SendEmailViewModel viewModel)
                return [];

            var command = new SendEmailCommand(viewModel.To, viewModel.Subject, viewModel.Body);

            var result = await _commandValidator.ValidateAsync(
                ValidationContext<SendEmailCommand>
                    .CreateWithOptions(command, strategy => strategy.IncludeProperties(propertyName))
            );

            return result.IsValid ? [] : result.Errors.Select(error => error.ErrorMessage);
        };
    }
}