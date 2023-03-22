using System;
using App.Monitoring.Entities.Entities;

namespace App.Monitoring.UseCases.Handlers.Nodes.Queries.GetNode;

/// <summary>
/// Модель запроса получения узла.
/// </summary>
/// <param name="Id">Идентификатор узла.</param>
public sealed record GetNodeQuery(Guid Id) : IQuery<NodeEntity?>;
