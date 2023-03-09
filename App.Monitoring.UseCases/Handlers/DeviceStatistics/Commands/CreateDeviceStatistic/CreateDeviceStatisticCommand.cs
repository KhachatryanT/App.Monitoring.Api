using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Commands.CreateDeviceStatistic;

/// <summary>
/// Модель создания нового узла.
/// </summary>
public sealed class CreateDeviceStatisticCommand: ICommand
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
    public string? UserName { get; init; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    public string? ClientVersion { get; init; }
}
