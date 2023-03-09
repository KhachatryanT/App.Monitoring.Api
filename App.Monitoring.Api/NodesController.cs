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
[Route("nodes")]
[ApiController]
public class NodesController : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// <see cref="NodesController"/>.
    /// </summary>
    /// <param name="sender">MediatR.</param>
    public NodesController(ISender sender) => _sender = sender;

    /// <summary>
    /// Получить узлы.
    /// </summary>
    /// <returns>Коллекция узлов.</returns>
    [HttpGet]
    public async IAsyncEnumerable<NodeResult> GetNodes()
    {
        var nodes = _sender.CreateStream(new GetDeviceStatisticsQuery());
        await foreach (var node in nodes)
        {
            yield return new NodeResult
            {
                Name = node.UserName,
                Date = node.Date,
                ClientVersion = node.ClientVersion,
                DeviceType = node.DeviceType
            };
        }
    }

    /// <summary>
    /// Добавить новый узел.
    /// </summary>
    /// <param name="node">Узел для добавления.</param>
    /// <returns>Ok.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateNode(
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequestBodyNotSpecified")]
        CreateNodeRequest node)
    {
        await _sender.Send(new CreateDeviceStatisticCommand
        {
            DeviceId = node.DeviceId,
            DeviceType = node.DeviceType,
            UserName = node.UserName,
            ClientVersion = node.ClientVersion,
        });
        return Ok();
    }
}
