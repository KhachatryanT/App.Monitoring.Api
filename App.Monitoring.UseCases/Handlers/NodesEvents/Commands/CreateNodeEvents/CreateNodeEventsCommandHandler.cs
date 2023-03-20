using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.NodesEvents.Commands.CreateNodeEvents;

/// <summary>
/// Обработчик команды создания новых событий узла.
/// </summary>
internal sealed class CreateNodeEventsCommandHandler : ICommandHandler<CreateNodeEventsCommand>
{
    private readonly INodeEventsRepository _nodeEventsRepository;
    private readonly INodesRepository _nodesRepository;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="nodesRepository">Репозиторий узлов.</param>
    /// <param name="nodeEventsRepository">Репозиторий событий узлов.</param>
    public CreateNodeEventsCommandHandler(INodesRepository nodesRepository,
        INodeEventsRepository nodeEventsRepository)
    {
        _nodesRepository = nodesRepository;
        _nodeEventsRepository = nodeEventsRepository;
    }

    /// <inheritdoc/>
    public async Task Handle(CreateNodeEventsCommand request, CancellationToken cancellationToken)
    {
        var node = await _nodesRepository.GetAsync(request.NodeId, cancellationToken);
        if (node is null)
        {
            node = new NodeEntity(request.NodeId, DeviceType.Unknown, default, default, default);
            await _nodesRepository.CreateAsync(node, cancellationToken);
        }

        var dto = request.Events.Select(x => new NodeEventEntity(request.NodeId, x.Name, x.Date));
        await _nodeEventsRepository.CreateAsync(dto, cancellationToken);
    }
}
