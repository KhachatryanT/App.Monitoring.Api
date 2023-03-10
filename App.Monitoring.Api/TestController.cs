using System;
using System.Threading.Tasks;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Api;

/// <summary>
/// Вспомогательный контроллерр.
/// </summary>
/// <remarks>
/// Здесь находятся вспомогательные утилиты. Не для ревью.
/// </remarks>
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IDevicesStatisticsRepository _repository;


    /// <summary>
    /// <see cref="TestController"/>.
    /// </summary>
    /// <param name="repository">Репозиторий статистики устройств.</param>
    public TestController(IDevicesStatisticsRepository repository) => _repository = repository;

    /// <summary>
    /// Наполнить БД случайными данными.
    /// </summary>
    /// <param name="itemsCount">Количество записей для добавления.</param>
    /// <returns>Ok.</returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> FillDatabase(int itemsCount = 10)
    {
        for (var i = 0; i < itemsCount; i++)
        {
            var deviceStatistic = new DeviceStatistic(Guid.NewGuid(),
                (DeviceType)new Random(Environment.TickCount).Next(0, 3),
                $"User name {i}",
                $"ClientVersion {i}",
                DateTimeOffset.UtcNow);
            await _repository.CreateDeviceStatisticAsync(deviceStatistic);
            await Task.Delay(1);
        }

        return Ok();
    }

    /// <summary>
    /// Выбросить исключение.
    /// </summary>
    /// <returns>Исключение.</returns>
    /// <exception cref="Exception">Исключение.</exception>
    [HttpGet("[action]")]
    public IActionResult ThrowException() => throw new Exception("Requested exception was thrown");
}
