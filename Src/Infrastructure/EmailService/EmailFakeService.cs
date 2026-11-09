
using Infrastructure.Velidator;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EmailService;

/// <inheritdoc/>
internal sealed class EmailFakeService : IEmailService
{
    readonly IValidator<EmailMessage> _validator;
    readonly ILogger<EmailFakeService> _logger;

    public EmailFakeService(IValidator<EmailMessage> validator, ILogger<EmailFakeService> logger)
    {
        _validator = validator;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task Send(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow(emailMessage);
        _logger.LogInformation($"Sending... email.");
        await Task.Delay(500).ConfigureAwait(false);
        _logger.LogInformation($"Email sended.");
    }
}