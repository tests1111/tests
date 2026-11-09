using System.Collections.ObjectModel;
using Infrastructure.EmailService;
using Infrastructure.Velidator;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Extension
{
    public static void AddEmail(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddSingleton<IEmailService, EmailFakeService>();
        services.AddSingleton(typeof(IValidator<EmailMessage>), typeof(EmailMessageValidator));
    }
}