using System.Threading.Tasks;
using App.Monitoring.Entities.Contracts;
using App.Monitoring.Infrastructure.Implementation.Hubs;
using App.Monitoring.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace App.Monitoring.Infrastructure.Implementation.Observers;

/// <summary>
/// Наблюдатель за изменениями в UnitOfWork.
/// Отправляет событие о завершении в Notification Hub.
/// </summary>
internal sealed class SendToNotificationHubCompletedObserver : IUnitOfWorkCompletedObserver
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="hubContext">Hub уведомлений.</param>
    public SendToNotificationHubCompletedObserver(IHubContext<NotificationHub, INotificationClient> hubContext) => _hubContext = hubContext;

    /// <inheritdoc/>
    public async Task Next() => await _hubContext.Clients.All.UnitOfWorkIsCompleted();
}
