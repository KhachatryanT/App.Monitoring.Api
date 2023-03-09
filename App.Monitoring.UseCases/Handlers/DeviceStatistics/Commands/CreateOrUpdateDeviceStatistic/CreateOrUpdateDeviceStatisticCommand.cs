using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Commands.CreateOrUpdateDeviceStatistic;

/// <summary>
/// Модель создания новой статистики устройства.
/// </summary>
public sealed class CreateOrUpdateDeviceStatisticCommand: ICommand
{
    /// <summary>
    /// Идентификатор устройства.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Тип устройства.
    /// </summary>
    public DeviceType DeviceType { get; init; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    public string? ClientVersion { get; init; }
}
