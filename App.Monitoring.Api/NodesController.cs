using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateOrUpdateNode;
using App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNode;
using App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    /// <returns>Узлы.</returns>
    [HttpGet]
    public async Task<IEnumerable<Node>> GetNodes()
    {
        var nodes = await _sender.Send(new GetNodesQuery());
        return nodes.Adapt<Node[]>();
    }

    /// <summary>
    /// Получить узлел.
    /// </summary>
    /// <returns>Узел.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Node>> GetNode(Guid id)
    {
        var nodes = await _sender.Send(new GetNodeQuery(id));
        return nodes is null ? NotFound() : nodes.Adapt<Node>();
    }

    /// <summary>
    /// Добавить или обновить узел.
    /// </summary>
    /// <param name="id">Идентификатор устройства.</param>
    /// <param name="node">Узел для добавления/обновления.</param>
    /// <returns>Ok.</returns>
    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateOrUpdateNode(Guid id, [Required] CreateNodeRequest node)
    {
        await _sender.Send(new CreateOrUpdateNodeCommand
        (
            id,
            node.DeviceType,
            node.UserName,
            node.ClientVersion
        ));
        return Ok();
    }
}
