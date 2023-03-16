using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;

namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Репозиторий событий узлов.
/// </summary>
public interface INodesEventsRepository
{
    /// <summary>
    /// Получить события по идентификатору узла.
    /// </summary>
    /// <param name="nodeId">Идентификатор узла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>События.</returns>
    Task<IEnumerable<NodeEventEntity>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken);

    /// <summary>
    /// Создать события.
    /// </summary>
    /// <param name="events">События.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task CreateAsync(IEnumerable<NodeEventEntity> events, CancellationToken cancellationToken);
}
