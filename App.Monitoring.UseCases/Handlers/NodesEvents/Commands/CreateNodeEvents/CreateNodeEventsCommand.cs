using System;
using System.Collections.Generic;
using App.Monitoring.UseCases.Dto;

namespace App.Monitoring.UseCases.Handlers.NodesEvents.Commands.CreateNodeEvents;

/// <summary>
/// Модель команды создания новых событий узла.
/// </summary>
/// <param name="NodeId">Идентификатор узла.</param>
/// <param name="Events">События.</param>
public sealed record CreateNodeEventsCommand(Guid NodeId, IEnumerable<NodeEventDto> Events) : ICommand;
