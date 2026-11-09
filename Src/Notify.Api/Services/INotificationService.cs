using Infrastructure.Models;

namespace Notify.Api.Services;

public interface INotificationService
{
    Task<bool> SendNotify(NotificationItemModel item);
}