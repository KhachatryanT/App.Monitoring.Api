using System;
using System.Collections.Generic;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Модель результата получения событий узла.
/// </summary>
/// <param name="Id">Идентификатор узла.</param>
/// <param name="Events">События.</param>
public sealed record GetNodeEventsResult(Guid Id, IEnumerable<NodeEvent> Events);
