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
    private static readonly ConcurrentDictionary<Guid, DeviceStatistic> cache = new();

    /// <summary>
    /// Получить узлы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция узлов.</returns>
    public async IAsyncEnumerable<DeviceStatistic> GetDevicesStatisticsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var (_, value) in cache)
        {
            await Task.Delay(1, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            yield return value;
        }
    }

    /// <summary>
    /// Получить статистику устройства.
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистика устройства.</returns>
    /// <exception cref="ArgumentException">Не удалось найти запись.</exception>
    public Task<DeviceStatistic> GetDeviceStatisticAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(deviceId, out var deviceStatistic) || deviceStatistic is null)
        {
            throw new ArgumentException("Не удалось найти запись", nameof(deviceId));
        }

        return Task.FromResult(deviceStatistic);
    }

    /// <summary>
    /// Создать новую статистику устройства.
    /// </summary>
    /// <param name="deviceStatistic">Статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно добавить статистику устройства. Статистика уже существует.</exception>
    public Task CreateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default)
    {
        if (!cache.TryAdd(deviceStatistic.Id, deviceStatistic))
        {
            throw new ArgumentException("Не удалось добавить запись", nameof(deviceStatistic));
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновление статистики устройства.
    /// </summary>
    /// <param name="deviceStatistic">Статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно обновить статистику устройства. Возможно статистики с указанным ключом не существует.</exception>
    public Task UpdateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default)
    {
        if (!cache.TryGetValue(deviceStatistic.Id, out var existingDeviceStatistic))
        {
            throw new ArgumentException("Не удалось найти запись по указанному ключу", nameof(deviceStatistic.Id));
        }

        if (!cache.TryUpdate(deviceStatistic.Id, deviceStatistic, existingDeviceStatistic))
        {
            throw new ArgumentException("Не удалось обновить запись", nameof(deviceStatistic));
        }

        return Task.CompletedTask;
    }
}
