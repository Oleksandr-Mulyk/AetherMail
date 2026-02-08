namespace AetherMail.Contracts
{
    public record SendEmailCommand(string To, string Subject, string Body);
}
