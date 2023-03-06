using System.Collections.Generic;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;

/// <summary>
/// Запрос получения узлов
/// </summary>
public sealed class GetNodesQuery : IQuery<IAsyncEnumerable<Node>>
{
}
