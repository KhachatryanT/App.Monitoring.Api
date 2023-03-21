using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;

/// <summary>
/// Обработчик запроса получения узлов.
/// </summary>
internal sealed class GetNodesQueryHandler : IQueryHandler<GetNodesQuery, IEnumerable<NodeEntity>>
{
    private readonly INodesRepository _nodesRepository;

    /// <summary>
    /// <see cref="GetNodesQueryHandler"/>.
    /// </summary>
    /// <param name="nodesRepository">Репозиторий узлов.</param>
    public GetNodesQueryHandler(INodesRepository nodesRepository) => _nodesRepository = nodesRepository;

    /// <inheritdoc/>
    public async Task<IEnumerable<NodeEntity>> Handle(GetNodesQuery request, CancellationToken cancellationToken) =>
        await _nodesRepository.GetAsync(cancellationToken);
}
