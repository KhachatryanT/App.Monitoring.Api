using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using JetBrains.Annotations;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;

/// <summary>
/// Обработчик запроса получения узлов.
/// </summary>
[UsedImplicitly]
internal sealed class GetNodesQueryHandler : IQueryHandler<GetNodesQuery, IAsyncEnumerable<Node>>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// <see cref="GetNodesQueryHandler"/>.
    /// </summary>
    /// <param name="dbContext">БД контекст.</param>
    public GetNodesQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public Task<IAsyncEnumerable<Node>> Handle(GetNodesQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(_dbContext.Nodes.AsAsyncEnumerable());
}
