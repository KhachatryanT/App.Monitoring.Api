using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Транзакционная работа с репозиториями.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
    /// <summary>
    /// Репозиторий событий узлов.
    /// </summary>
    INodeEventsRepository NodeEventsRepository { get; }

    /// <summary>
    /// Репозиторий узлов.
    /// </summary>
    INodesRepository NodesRepository { get; }

    /// <summary>
    /// Сохранение данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
