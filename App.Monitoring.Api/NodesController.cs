using System.Collections.Generic;
using System.Threading.Tasks;
using App.Monitoring.Api.Contracts;
using App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;
using Mapster;
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
    /// <returns>Узлы.</returns>
    [HttpGet]
    public async Task<IEnumerable<Node>> GetNodes()
    {
        var nodes = await _sender.Send(new GetNodesQuery());
        return nodes.Adapt<Node[]>();
    }
}
