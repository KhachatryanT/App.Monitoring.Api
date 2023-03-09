using System;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistic;

/// <summary>
/// Запрос получения узла.
/// </summary>
public sealed record GetDeviceStatisticQuery : IQuery<DeviceStatistic>
{
    /// <summary>
    /// <see cref="GetDeviceStatisticQuery"/>.
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства.</param>
    public GetDeviceStatisticQuery(Guid deviceId) => DeviceId = deviceId;

    /// <summary>
    /// Идентификатор устройства.
    /// </summary>
    public Guid DeviceId { get; }
}
