using System;
using System.Collections.Generic;
using App.Monitoring.Entities.Entities;

namespace App.Monitoring.UseCases.Handlers.NodesEvents.Queries.GetNodeEvents;

/// <summary>
/// Модель запроса получения событий узла.
/// </summary>
/// <param name="NodeId">Идентификатор узла.</param>
public sealed record GetNodeEventsQuery(Guid NodeId) : IQuery<IEnumerable<NodeEventEntity>>;
