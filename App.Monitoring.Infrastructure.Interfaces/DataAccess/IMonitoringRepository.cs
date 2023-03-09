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
    /// Получить все узлы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллеция узлов.</returns>
    IAsyncEnumerable<DeviceStatistic> GetDevicesStatisticsAsync(CancellationToken cancellationToken);


    /// <summary>
    /// Получить статистику устройства.
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статистика устройства.</returns>
    Task<DeviceStatistic> GetDeviceStatisticAsync(Guid deviceId, CancellationToken cancellationToken);

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
    /// <param name="deviceStatistic">Статистика устройства.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task UpdateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default);
}
