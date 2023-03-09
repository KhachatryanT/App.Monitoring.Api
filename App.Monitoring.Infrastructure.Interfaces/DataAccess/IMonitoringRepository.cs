using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Репозиторий данных.
/// </summary>
public interface IMonitoringRepository
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
    /// <param name="newDeviceStatistic">Новая статистика устройства.</param>
    /// <param name="oldDeviceStatistic">Существующая статистика устройства. Необходима для сверки с хранимыми данными.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task UpdateDeviceStatisticAsync(DeviceStatistic newDeviceStatistic, DeviceStatistic oldDeviceStatistic, CancellationToken cancellationToken = default);
}
