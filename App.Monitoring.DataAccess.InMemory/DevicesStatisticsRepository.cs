using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
    public async Task<IEnumerable<DeviceStatistic>> GetDevicesStatisticsAsync(CancellationToken cancellationToken) =>
        await Task.FromResult(cache.Values);

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
    /// <param name="deviceStatistic">Новая статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    /// <exception cref="ArgumentException">Невозможно обновить статистику устройства. Возможно статистики с указанным ключом не существует.</exception>
    public async Task UpdateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default)
    {
        var existingStatistic = await GetDeviceStatisticOrDefaultAsync(deviceStatistic.Id, cancellationToken);
        if (existingStatistic is null)
        {
            throw new ArgumentException("Не удалось найти запись для обновления", nameof(deviceStatistic));
        }

        if (!cache.TryUpdate(deviceStatistic.Id, deviceStatistic, existingStatistic))
        {
            throw new ArgumentException("Не удалось обновить запись", nameof(deviceStatistic));
        }
    }
}
