using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Узел.
/// </summary>
public sealed class NodeResult
{
    /// <summary>
    /// Тип устройства.
    /// </summary>
    public DeviceType DeviceType { get; set; }

    /// <summary>
    /// Имя узла.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Дата статистики.
    /// </summary>
    public DateTimeOffset StatisticDate { get; set; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    public string? ClientVersion { get; set; }
}
