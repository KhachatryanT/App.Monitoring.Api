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
    private readonly INodesRepository _nodesRepository;
    private readonly INodesEventsRepository _nodesEventsRepository;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="nodesRepository">Репозиторий узлов.</param>
    /// <param name="nodesEventsRepository">Репозиторий событий узлов.</param>
    public CreateNodeEventsCommandHandler(INodesRepository nodesRepository,
        INodesEventsRepository nodesEventsRepository)
    {
        _nodesRepository = nodesRepository;
        _nodesEventsRepository = nodesEventsRepository;
    }

    /// <inheritdoc/>
    public async Task Handle(CreateNodeEventsCommand request, CancellationToken cancellationToken)
    {
        var node = await _nodesRepository.GetAsync(request.NodeId, cancellationToken);
        if (node is null)
        {
            node = new NodeEntity(request.NodeId, DeviceType.Unknown, default, default, default);
            await _nodesRepository.InsertAsync(node, cancellationToken);
        }

        var dto = request.Events.Select(x => new NodeEventEntity(request.NodeId, x.Name, x.Date));
        await _nodesEventsRepository.CreateAsync(dto, cancellationToken);
    }
}
