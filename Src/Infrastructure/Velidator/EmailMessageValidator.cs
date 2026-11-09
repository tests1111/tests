using Infrastructure.EmailService;

namespace Infrastructure.Velidator;

public interface IValidator<T>
{
    void ValidateAndThrow(T data);
}

internal class EmailMessageValidator : IValidator<EmailMessage>
{
    public void ValidateAndThrow(EmailMessage data)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(data.From);
        ArgumentNullException.ThrowIfNull(data.To);
        ArgumentNullException.ThrowIfNull(data.Subject);
        ArgumentNullException.ThrowIfNull(data.Body);
    }
}