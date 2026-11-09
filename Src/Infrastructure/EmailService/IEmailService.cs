namespace Infrastructure.EmailService;

public interface IEmailService
{
    Task Send(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}