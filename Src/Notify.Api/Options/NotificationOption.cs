using Infrastructure.Models;

namespace Notify.Api.Options;

public sealed class NotificationOption
{
    public IReadOnlyDictionary<PersonType, NotificationProfile> Profiles { set; get; } = default!;
}

public sealed class NotificationProfile
{
    public string To { set; get; } = default!;

    public string Subject { set; get; } = default!;

    public string Body { set; get; } = default!;
}