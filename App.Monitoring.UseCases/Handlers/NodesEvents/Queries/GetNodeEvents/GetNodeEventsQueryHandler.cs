using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.NodesEvents.Queries.GetNodeEvents;

/// <summary>
/// Обработчик запроса получения событий узла.
/// </summary>
internal sealed class GetNodeEventsQueryHandler : IQueryHandler<GetNodeEventsQuery, IEnumerable<NodeEventEntity>>
{
    private readonly INodesEventsRepository _nodesEventsRepository;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="nodesEventsRepository">Репозиторий событий узлов.</param>
    public GetNodeEventsQueryHandler(INodesEventsRepository nodesEventsRepository) => _nodesEventsRepository = nodesEventsRepository;

    /// <inheritdoc/>
    public async Task<IEnumerable<NodeEventEntity>> Handle(GetNodeEventsQuery request, CancellationToken cancellationToken) =>
        await _nodesEventsRepository.GetByNodeIdAsync(request.NodeId, cancellationToken);
}
