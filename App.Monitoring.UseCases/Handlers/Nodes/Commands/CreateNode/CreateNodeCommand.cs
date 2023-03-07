using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateNode;

/// <summary>
/// Модель создания нового узла.
/// </summary>
public sealed class CreateNodeCommand: ICommand
{
    /// <summary>
    /// Идентификатор устройства.
    /// </summary>
    public Guid DeviceId { get; init; }

    /// <summary>
    /// Тип устройства.
    /// </summary>
    public DeviceType DeviceType { get; init; }

    /// <summary>
    /// Имя узла.
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    /// Дата последней статистики.
    /// </summary>
    public DateTimeOffset Date { get; init; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    public string ClientVersion { get; init; } = default!;
}
