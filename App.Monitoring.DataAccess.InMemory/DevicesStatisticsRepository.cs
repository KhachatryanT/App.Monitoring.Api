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
/// Репозиторий статистики устройств.
/// </summary>
internal sealed class DevicesStatisticsRepository : IDevicesStatisticsRepository
{
    private static readonly ConcurrentDictionary<Guid, DeviceStatistic> cache = new();

    /// <summary>
    /// Получить статистики устройств.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистики устройств.</returns>
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
    /// <param name="id">Идентификатор устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистика устройства.</returns>
    public Task<DeviceStatistic?> GetDeviceStatisticOrDefaultAsync(Guid id, CancellationToken cancellationToken)
    {
        cache.TryGetValue(id, out var deviceStatistic);
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
    /// Обновить статистику устройства.
    /// </summary>
    /// <param name="newDeviceStatistic">Новая статистика устройства.</param>
    /// <param name="oldDeviceStatistic">Существующая статистика устройства. Необходима для сверки с хранимыми данными.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно обновить статистику устройства. Возможно статистики с указанным ключом не существует.</exception>
    public Task UpdateDeviceStatisticAsync(DeviceStatistic newDeviceStatistic, DeviceStatistic oldDeviceStatistic, CancellationToken cancellationToken = default)
    {
        if (!cache.TryUpdate(newDeviceStatistic.Id, newDeviceStatistic, oldDeviceStatistic))
        {
            throw new ArgumentException("Не удалось обновить запись", nameof(newDeviceStatistic));
        }

        return Task.CompletedTask;
    }
}
