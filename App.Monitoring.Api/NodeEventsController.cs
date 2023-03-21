using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Dto;
using App.Monitoring.UseCases.Handlers.NodesEvents.Commands.CreateNodeEvents;
using App.Monitoring.UseCases.Handlers.NodesEvents.Queries.GetNodeEvents;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Api;

/// <summary>
/// Методы работы с событиями узла.
/// </summary>
[Route("nodes/{nodeId:guid}/events")]
[ApiController]
public class NodeEventsController : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="sender">MediatR.</param>
    public NodeEventsController(ISender sender) => _sender = sender;

    /// <summary>
    /// Добавить события узла.
    /// </summary>
    /// <param name="nodeId">Идентификатор узла.</param>
    /// <param name="events">События.</param>
    /// <returns>Ok.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNodeEvents(Guid nodeId, [Required] IEnumerable<NodeEvent> events)
    {
        await _sender.Send(new CreateNodeEventsCommand(nodeId, events.Adapt<NodeEventDto[]>()));
        return Ok();
    }

    /// <summary>
    /// Получить события узла.
    /// </summary>
    /// <param name="nodeId">Идентификатор узла.</param>
    /// <returns>Ok.</returns>
    [HttpGet]
    public async Task<GetNodeEventsResult> GetNodeEvents(Guid nodeId)
    {
        var events = await _sender.Send(new GetNodeEventsQuery(nodeId));
        return new GetNodeEventsResult(nodeId, events.Adapt<NodeEvent[]>());
    }
}
