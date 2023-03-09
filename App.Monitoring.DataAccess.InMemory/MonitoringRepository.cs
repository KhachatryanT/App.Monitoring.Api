using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.DataAccess.InMemory;

/// <summary>
/// Репозиторий данных.
/// </summary>
internal sealed class MonitoringRepository : IMonitoringRepository
{
    private static readonly ConcurrentDictionary<Guid, Node> cache = new();

    /// <summary>
    /// Получить узлы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция узлов.</returns>
#pragma warning disable CS1998
    public async IAsyncEnumerable<Node> GetNodesAsync([EnumeratorCancellation] CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        foreach (var (_, value) in cache)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return value;
        }
    }

    /// <summary>
    /// Получить узел.
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Узел.</returns>
    /// <exception cref="ArgumentException">Не удалось найти запись.</exception>
    public Task<Node> GetNodeAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(deviceId, out var node))
        {
            throw new ArgumentException("Не удалось найти запись", nameof(deviceId));
        }

#pragma warning disable CS8619
        return Task.FromResult(node);
#pragma warning restore CS8619
    }

    /// <summary>
    /// Создать новый узел.
    /// </summary>
    /// <param name="node">Узел.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно добавить узел. Узел уже существует.</exception>
    public Task CreateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        if (!cache.TryAdd(node.DeviceId, node))
        {
            throw new ArgumentException("Не удалось добавить запись", nameof(node));
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновление узла.
    /// </summary>
    /// <param name="node">Узел.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно обновить узел. Возможно узел с указанным ключом не существует.</exception>
    public Task UpdateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        if (!cache.TryGetValue(node.DeviceId, out var existingNode))
        {
            throw new ArgumentException("Не удалось найти запись по указанному ключу", nameof(node.DeviceId));
        }

        if (!cache.TryUpdate(node.DeviceId, node, existingNode))
        {
            throw new ArgumentException("Не удалось обновить запись", nameof(node));
        }

        return Task.CompletedTask;
    }
}
