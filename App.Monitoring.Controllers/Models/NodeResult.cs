using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Controllers.Models;

/// <summary>
/// Узел.
/// </summary>
public class NodeResult
{
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
