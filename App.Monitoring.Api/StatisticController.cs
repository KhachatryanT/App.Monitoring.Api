using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Handlers.DeviceStatistics.Commands.CreateOrUpdateDeviceStatistic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Api;

/// <summary>
/// Методы работы со статистикой устройства.
/// </summary>
[Route("statistics/{id:guid}")]
[ApiController]
public class StatisticController : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// <see cref="StatisticController"/>.
    /// </summary>
    /// <param name="sender">MediatR.</param>
    public StatisticController(ISender sender) => _sender = sender;

    /// <summary>
    /// Добавить или обновить статистику устройства.
    /// </summary>
    /// <param name="id">Идентификатор устройства.</param>
    /// <param name="deviceStatistic">Статистика устройства для добавления/обновления.</param>
    /// <returns>Ok.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateDeviceStatistic(Guid id, [Required] CreateDeviceStatisticRequest deviceStatistic)
    {
        await _sender.Send(new CreateOrUpdateDeviceStatisticCommand
        {
            Id = id,
            DeviceType = deviceStatistic.DeviceType,
            UserName = deviceStatistic.UserName,
            ClientVersion = deviceStatistic.ClientVersion,
        });
        return Ok();
    }
}
