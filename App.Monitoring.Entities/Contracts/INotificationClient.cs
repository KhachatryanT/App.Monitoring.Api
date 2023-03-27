using System.Threading.Tasks;

namespace App.Monitoring.Entities.Contracts;

/// <summary>
/// Интерфейс клиента SignalR.
/// </summary>
public interface INotificationClient
{
    /// <summary>
    /// Произошло обновление узлов.
    /// </summary>
    /// <returns>Task.</returns>
    Task NodesModified();
}
