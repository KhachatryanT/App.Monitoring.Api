using System.Threading.Tasks;

namespace App.Monitoring.Entities.Contracts;

/// <summary>
/// Интерфейс клиента SignalR.
/// </summary>
public interface INotificationClient
{
    /// <summary>
    /// Произошло завершение unit of work.
    /// </summary>
    /// <returns>Task.</returns>
    Task UnitOfWorkIsCompleted();
}
