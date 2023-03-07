using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;

/// <summary>
/// Обработчик запроса получения узлов.
/// </summary>
// ReSharper disable once UnusedType.Global
internal sealed class GetNodesQueryHandler : IQueryHandler<GetNodesQuery, IAsyncEnumerable<Node>>
{
    private readonly IMonitoringRepository _repository;

    /// <summary>
    /// <see cref="GetNodesQueryHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий данных.</param>
    public GetNodesQueryHandler(IMonitoringRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public Task<IAsyncEnumerable<Node>> Handle(GetNodesQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(_repository.GetNodesAsync());
}
