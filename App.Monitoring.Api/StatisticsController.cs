using System.Collections.Generic;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Api;

/// <summary>
/// Методы работы со статистиками устройств.
/// </summary>
[Route("statistics")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// <see cref="StatisticsController"/>.
    /// </summary>
    /// <param name="sender">MediatR.</param>
    public StatisticsController(ISender sender) => _sender = sender;

    /// <summary>
    /// Получить статистики устройств.
    /// </summary>
    /// <returns>Статистики устройств.</returns>
    [HttpGet]
    public async IAsyncEnumerable<DeviceStatisticResult> GetDevicesStatistics()
    {
        var devicesStatistics = _sender.CreateStream(new GetDeviceStatisticsQuery());
        await foreach (var devicesStatistic in devicesStatistics)
        {
            yield return devicesStatistic.Adapt<DeviceStatisticResult>();
        }
    }
}
