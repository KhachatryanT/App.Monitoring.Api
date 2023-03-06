using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Monitoring.Controllers.Models;
using App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateNode;
using App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;
using App.Monitoring.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Controllers;

/// <summary>
/// Методы работы с узлами
/// </summary>
[Route("node")]
[ApiController]
public class NodesController : ControllerBase
{
    private readonly ISender _sender;

    /// <inheritdoc />
    public NodesController(ISender sender) => _sender = sender;

    /// <summary>
    /// Получить узлы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async IAsyncEnumerable<NodeResult> GetNodes()
    {
        var nodes = await _sender.Send(new GetNodesQuery());
        await foreach (var node in nodes)
        {
            yield return new NodeResult
            {
                Name = node.Name,
                Date = node.Date,
                ClientVersion = node.ClientVersion,
                DeviceType = node.DeviceType
            };
        }
    }

    /// <summary>
    /// Добавить новый узел
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateNode(
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequestBodyNotSpecified")]
        CreateNodeRequest node)
    {
        await _sender.Send(new CreateNodeCommand
        {
            DeviceId = node.DeviceId!.Value,
            DeviceType = node.DeviceType!.Value,
            Name = node.Name!,
            Date = node.Date!.Value,
            ClientVersion = node.ClientVersion!
        });
        return Ok();
    }
}
