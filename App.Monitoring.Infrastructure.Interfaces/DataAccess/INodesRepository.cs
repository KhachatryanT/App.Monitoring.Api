using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;

namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Репозиторий узлов.
/// </summary>
public interface INodesRepository
{
    /// <summary>
    /// Создать узел.
    /// </summary>
    /// <param name="nodeEntity">Узел.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task CreateAsync(NodeEntity nodeEntity, CancellationToken cancellationToken);

    /// <summary>
    /// Получить все узлы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Узлы.</returns>
    Task<IEnumerable<NodeEntity>> GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить узел.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Узел.</returns>
    Task<NodeEntity?> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить узел.
    /// </summary>
    /// <param name="nodeEntity">Узел.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task UpdateAsync(NodeEntity nodeEntity, CancellationToken cancellationToken);
}
