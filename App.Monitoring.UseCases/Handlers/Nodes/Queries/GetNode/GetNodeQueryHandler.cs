using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNode;

/// <summary>
/// Обработчик запроса получения узлов.
/// </summary>
internal sealed class GetNodeQueryHandler : IQueryHandler<GetNodeQuery, NodeEntity?>
{
    private readonly INodesRepository _nodesRepository;

    /// <summary>
    /// <see cref="GetNodesQueryHandler"/>.
    /// </summary>
    /// <param name="nodesRepository">Репозиторий узлов.</param>
    public GetNodeQueryHandler(INodesRepository nodesRepository) => _nodesRepository = nodesRepository;

    /// <inheritdoc/>
    public async Task<NodeEntity?> Handle(GetNodeQuery request, CancellationToken cancellationToken) =>
        await _nodesRepository.GetAsync(request.Id, cancellationToken);
}
