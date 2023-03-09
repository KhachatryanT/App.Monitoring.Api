using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Handlers.DeviceStatistics.Commands.CreateDeviceStatistic;
using App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;
using App.Monitoring.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Api;

/// <summary>
/// Методы работы с узлами.
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
    /// Получить узлы.
    /// </summary>
    /// <returns>Коллекция узлов.</returns>
    [HttpGet]
    public async IAsyncEnumerable<DeviceStatisticResult> GetDevicesStatistics()
    {
        var devicesStatistics = _sender.CreateStream(new GetDeviceStatisticsQuery());
        await foreach (var deviceStatistic in devicesStatistics)
        {
            yield return new DeviceStatisticResult
            {
                Name = deviceStatistic.UserName,
                StatisticDate = deviceStatistic.StatisticDate,
                ClientVersion = deviceStatistic.ClientVersion,
                DeviceType = deviceStatistic.DeviceType
            };
        }
    }

    /// <summary>
    /// Добавить новую статистику устройства.
    /// </summary>
    /// <param name="deviceStatistic">Статистика устройства для добавления.</param>
    /// <returns>Ok.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateDeviceStatistic(
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequestBodyNotSpecified")]
        CreateDeviceStatisticRequest deviceStatistic)
    {
        await _sender.Send(new CreateDeviceStatisticCommand
        {
            DeviceId = deviceStatistic.DeviceId,
            DeviceType = deviceStatistic.DeviceType,
            UserName = deviceStatistic.UserName,
            ClientVersion = deviceStatistic.ClientVersion,
        });
        return Ok();
    }
}
