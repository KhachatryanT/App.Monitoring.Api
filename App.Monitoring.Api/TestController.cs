using System;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Entities.Enums;
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
    private readonly INodesRepository _repository;


    /// <summary>
    /// <see cref="TestController"/>.
    /// </summary>
    /// <param name="repository">Репозиторий узлов.</param>
    public TestController(INodesRepository repository) => _repository = repository;

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
            var node = new NodeEntity(Guid.NewGuid(),
                (DeviceType)new Random(Environment.TickCount).Next(0, 3),
                $"User name {i}",
                $"ClientVersion {i}",
                DateTimeOffset.UtcNow);
            await _repository.CreateAsync(node, HttpContext.RequestAborted);
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
