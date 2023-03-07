using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Репозиторий данных.
/// </summary>
public interface IMonitoringRepository
{
    /// <summary>
    /// Получить все узлы.
    /// </summary>
    /// <returns>Коллеция узлов.</returns>
    IAsyncEnumerable<Node> GetNodesAsync();

    /// <summary>
    /// Создать новый узел.
    /// </summary>
    /// <param name="node">Узел.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task CreateNodeAsync(Node node, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить узел.
    /// </summary>
    /// <param name="node">Узел.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task UpdateNodeAsync(Node node, CancellationToken cancellationToken = default);
}
