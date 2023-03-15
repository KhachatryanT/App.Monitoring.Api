using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.DataAccess.InMemory;

/// <summary>
/// Репозиторий статистики устройств.
/// </summary>
internal sealed class NodesRepository : INodesRepository
{
    private static readonly ConcurrentDictionary<Guid, NodeEntity> cache = new();

    /// <summary>
    /// Получить статистики устройств.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистики устройств.</returns>
    public async Task<IEnumerable<NodeEntity>> GetAsync(CancellationToken cancellationToken) =>
        await Task.FromResult(cache.Values);

    /// <summary>
    /// Получить статистику устройства.
    /// </summary>
    /// <param name="id">Идентификатор устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистика устройства.</returns>
    public Task<NodeEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        cache.TryGetValue(id, out var deviceStatistic);
        return Task.FromResult(deviceStatistic);
    }

    /// <summary>
    /// Создать новую статистику устройства.
    /// </summary>
    /// <param name="nodeEntity">Статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно добавить статистику устройства. Статистика уже существует.</exception>
    public Task InsertAsync(NodeEntity nodeEntity, CancellationToken cancellationToken = default)
    {
        if (!cache.TryAdd(nodeEntity.Id, nodeEntity))
        {
            throw new ArgumentException("Не удалось добавить запись", nameof(nodeEntity));
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить статистику устройства.
    /// </summary>
    /// <param name="nodeEntity">Новая статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно обновить статистику устройства. Возможно статистики с указанным ключом не существует.</exception>
    public async Task UpdateAsync(NodeEntity nodeEntity, CancellationToken cancellationToken = default)
    {
        var existingStatistic = await GetAsync(nodeEntity.Id, cancellationToken);
        if (existingStatistic is null)
        {
            throw new ArgumentException("Не удалось найти запись для обновления", nameof(nodeEntity));
        }

        if (!cache.TryUpdate(nodeEntity.Id, nodeEntity, existingStatistic))
        {
            throw new ArgumentException("Не удалось обновить запись", nameof(nodeEntity));
        }
    }
}
