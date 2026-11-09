using System.Threading.Channels;
using Infrastructure.EmailService;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Notify.Api.Options;

namespace Notify.Api.Services;

/// <inheritdoc/>
internal sealed class NotificationService : INotificationService
{
    readonly IEmailService _emailService;
    readonly NotificationOption _option;
    readonly ILogger<NotificationService> _logger;

    public NotificationService(
        IEmailService emailService,
        IOptions<NotificationOption> option,
        ILogger<NotificationService> logger)
    {
        ArgumentNullException.ThrowIfNull(emailService);
        ArgumentNullException.ThrowIfNull(option.Value?.Profiles);
        ArgumentNullException.ThrowIfNull(logger);

        _emailService = emailService;
        _option = option.Value;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> SendNotify(NotificationItemModel notificationItemModel)
    {
        ArgumentNullException.ThrowIfNull(notificationItemModel);

        var personTypes = new PersonType[] { PersonType.Default, notificationItemModel.PersonType };
        foreach(var personType in personTypes)
        {
            if(await TrySendNotify(notificationItemModel, _option.Profiles[personType]))
            {
                return true;
            }
        }
        return false;
    }

    async Task<bool> TrySendNotify(
        NotificationItemModel notificationItemModel,
        NotificationProfile notificationProfile)
    {
        try
        {
            await _emailService.Send(new EmailMessage 
            {
                From = notificationItemModel.Email,
                To = notificationProfile.To,
                Subject = notificationProfile.Subject,
                Body = string.Format(notificationProfile.Body,
                    notificationItemModel.Name,
                    notificationItemModel.Email,
                    notificationItemModel.Phone),
            });
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation($"Send canceled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Send error.");
            return false;
        }
        return true;
    }
}