using System;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistic;

/// <summary>
/// Запрос получения статистики устройства.
/// </summary>
public sealed record GetDeviceStatisticQuery : IQuery<DeviceStatistic?>
{
    /// <summary>
    /// <see cref="GetDeviceStatisticQuery"/>.
    /// </summary>
    /// <param name="id">Идентификатор устройства.</param>
    public GetDeviceStatisticQuery(Guid id) => Id = id;

    /// <summary>
    /// Идентификатор устройства.
    /// </summary>
    public Guid Id { get; }
}
