using System;
using System.Threading.Tasks;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Controllers;

/// <summary>
/// Вспомогательный контроллерр
/// </summary>
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IDbContext _dbContext;

    /// <inheritdoc />
    public TestController(IDbContext dbContext) => _dbContext = dbContext;

    /// <summary>
    /// Наполнить БД случайными данными
    /// </summary>
    /// <param name="itemsCount"></param>
    /// <returns></returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> FillDatabase(int itemsCount = 10)
    {
        for (var i = 0; i < itemsCount; i++)
        {
            _dbContext.Nodes.Add(new Node
            {
                Date = DateTime.UtcNow,
                Name = $"Name {i}",
                ClientVersion = $"ClientVersion {i}",
                DeviceId = Guid.NewGuid(),
                DeviceType = (DeviceType)new Random(Environment.TickCount).Next(0, 5)
            });
        }

        await _dbContext.SaveChangesAsync(HttpContext.RequestAborted);
        return Ok();
    }

    /// <summary>
    /// Выбросить исключение
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("[action]")]
    public IActionResult ThrowException()
    {
        throw new Exception("Requested exception was thrown");
    }
}
