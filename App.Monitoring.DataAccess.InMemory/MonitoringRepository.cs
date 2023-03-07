using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.DataAccess.InMemory;

/// <summary>
/// Репозиторий данных
/// </summary>
internal sealed class MonitoringRepository : IMonitoringRepository
{
    private static readonly ConcurrentDictionary<Guid, Node> cache = new();

#pragma warning disable CS1998
    public async IAsyncEnumerable<Node> GetNodesAsync()
#pragma warning restore CS1998
    {
        foreach (var (_, value) in cache)
        {
            yield return value;
        }
    }

    public Task CreateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        if (!cache.TryAdd(node.DeviceId, node))
        {
            throw new ArgumentException("Не удалось добавить запись");
        }
        return Task.CompletedTask;
    }

    public Task UpdateNodeAsync(Node node, CancellationToken cancellationToken = default)
    {
        if (!cache.TryUpdate(node.DeviceId, node, node))
        {
            throw new ArgumentException("Не удалось обновить запись");
        }
        return Task.CompletedTask;
    }
}
