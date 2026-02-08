using AetherMail.Contracts;
using AetherMail.Web.ViewModels;
using AetherMail.Web.ViewModels.Validators;
using MudBlazor;

namespace AetherMail.Web.Components.Pages
{
    public partial class Compose(IMessagePublisher publisher, ISnackbar snackbar)
    {
        private MudForm _form = null!;
        private readonly SendEmailViewModel _model = new();
        private readonly SendEmailViewModelValidator _validator = new();

        private async Task HandleSubmitAsync()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                var command = new SendEmailCommand(_model.To, _model.Subject, _model.Body);

                await publisher.PublishAsync(command);

                snackbar.Add("Message successfully sent to the queue!", Severity.Success);

                ResetForm();
            }
        }

        private void ResetForm()
        {
            _model.To = string.Empty;
            _model.Subject = string.Empty;
            _model.Body = string.Empty;
            _form.ResetAsync();
        }
    }
}
