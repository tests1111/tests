using Infrastructure;
using Notify.Api.Options;
using Notify.Api.Services;

namespace Notify.Api;

public static class Extension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddEmail();
        services.AddSingleton<INotificationService, NotificationService>();
        services.Configure<NotificationOption>(configuration.GetRequiredSection("Notification"));
    }
}