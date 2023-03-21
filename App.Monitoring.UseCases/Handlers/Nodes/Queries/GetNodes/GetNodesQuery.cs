using System.Collections.Generic;
using App.Monitoring.Entities.Entities;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNodes;

/// <summary>
/// Модель запроса получения узлов.
/// </summary>
public sealed record GetNodesQuery : IQuery<IEnumerable<NodeEntity>>;
