using System.Threading.Tasks;
using App.Monitoring.Entities.Contracts;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Implementation.Hubs;
using App.Monitoring.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace App.Monitoring.Infrastructure.Implementation.Observers;

/// <summary>
/// Наблюдатель за изменениями в узлах.
/// Отправляет событие об изменении в Notification Hub.
/// </summary>
internal sealed class NodesToNotificationHubEmitterObserver : IAppObserver<NodeEntity>
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="hubContext">Hub уведомлений.</param>
    public NodesToNotificationHubEmitterObserver(IHubContext<NotificationHub, INotificationClient> hubContext) => _hubContext = hubContext;

    /// <inheritdoc/>
    public async Task Next(NodeEntity obj) => await _hubContext.Clients.All.NodesModified();
}
