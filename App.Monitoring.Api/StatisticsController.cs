using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<IEnumerable<DeviceStatisticResult>> GetDevicesStatistics()
    {
        var devicesStatistics = await _sender.Send(new GetDeviceStatisticsQuery());
        return devicesStatistics.Adapt<DeviceStatisticResult[]>();
    }
}
