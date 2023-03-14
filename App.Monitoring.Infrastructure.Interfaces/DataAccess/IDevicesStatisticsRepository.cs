using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Репозиторий статистики устройств.
/// </summary>
public interface IDevicesStatisticsRepository
{
    /// <summary>
    /// Получить все статистики устройств.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистики устройств.</returns>
    IAsyncEnumerable<DeviceStatistic> GetDevicesStatisticsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить статистику устройства.
    /// </summary>
    /// <param name="id">Идентификатор устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистика устройства.</returns>
    Task<DeviceStatistic?> GetDeviceStatisticOrDefaultAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создать статистику устройства.
    /// </summary>
    /// <param name="deviceStatistic">Статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task CreateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить статистику устройства.
    /// </summary>
    /// <param name="deviceStatistic">Новая статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task UpdateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default);
}
