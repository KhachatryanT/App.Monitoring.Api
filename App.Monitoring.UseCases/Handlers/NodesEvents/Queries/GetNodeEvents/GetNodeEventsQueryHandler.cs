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
    private readonly INodeEventsRepository _nodeEventsRepository;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="nodeEventsRepository">Репозиторий событий узлов.</param>
    public GetNodeEventsQueryHandler(INodeEventsRepository nodeEventsRepository) => _nodeEventsRepository = nodeEventsRepository;

    /// <inheritdoc/>
    public async Task<IEnumerable<NodeEventEntity>> Handle(GetNodeEventsQuery request, CancellationToken cancellationToken) =>
        await _nodeEventsRepository.GetByNodeIdAsync(request.NodeId, cancellationToken);
}
