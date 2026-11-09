using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Notify.Api.Services;
using System.ComponentModel;

namespace Notify.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class NotificationController : ControllerBase
{
    readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        ArgumentNullException.ThrowIfNull(notificationService);
       _notificationService = notificationService;
    }

    [Description("Форма обратной связи по номеру.")]
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post([FromBody] NotificationItemModel model)
    {
        await _notificationService.SendNotify(model);
        return Ok();
    }
}
