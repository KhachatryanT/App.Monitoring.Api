using System.Threading.Tasks;
using App.Monitoring.Entities.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace App.Monitoring.Infrastructure.Implementation.Hubs;

/// <summary>
/// Хаб уведомлений.
/// </summary>
public class NotificationHub : Hub<INotificationClient>
{
    /// <summary>
    /// Отправить сообщения об обновлении узлов.
    /// </summary>
    /// <returns>Task.</returns>
    public async Task NodesUpdatedNotify() => await Clients.All.NodesModified();
}
