using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using JetBrains.Annotations;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;

[UsedImplicitly]
internal sealed class GetNodesQueryHandler : IQueryHandler<GetNodesQuery, IAsyncEnumerable<Node>>
{
    private readonly IDbContext _dbContext;

    public GetNodesQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

    public Task<IAsyncEnumerable<Node>> Handle(GetNodesQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(_dbContext.Nodes.AsAsyncEnumerable());
}
