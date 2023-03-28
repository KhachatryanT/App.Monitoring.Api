using System.Threading.Tasks;
using App.Monitoring.Entities.Contracts;
using App.Monitoring.Infrastructure.Implementation.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace App.Monitoring.Host;

/// <summary>
/// Вспомогательный контроллер.
/// </summary>
/// <remarks>
/// Временный контроллер. В последствии будет удален.
/// </remarks>
[Route("[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="hubContext">Хаб уведомлений.</param>
    public TestController(IHubContext<NotificationHub, INotificationClient> hubContext) => _hubContext = hubContext;

    /// <summary>
    /// Инициировать отправку sigralr сообщения.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status204NoContent"/>.</returns>
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> SendSignalrNotification()
    {
        await _hubContext.Clients.All.NodesModified();
        return NoContent();
    }
}
