using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateOrUpdateNode;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Monitoring.Api;

/// <summary>
/// Методы работы с узлом.
/// </summary>
[Route("nodes/{id:guid}")]
[ApiController]
public class NodeController : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// <see cref="NodeController"/>.
    /// </summary>
    /// <param name="sender">MediatR.</param>
    public NodeController(ISender sender) => _sender = sender;

    /// <summary>
    /// Добавить или обновить узел.
    /// </summary>
    /// <param name="id">Идентификатор устройства.</param>
    /// <param name="node">Узел для добавления/обновления.</param>
    /// <returns>Ok.</returns>
    [HttpPost]
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
