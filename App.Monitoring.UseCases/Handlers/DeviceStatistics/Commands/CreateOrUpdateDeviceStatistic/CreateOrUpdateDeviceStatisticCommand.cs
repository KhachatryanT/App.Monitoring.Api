using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Commands.CreateOrUpdateDeviceStatistic;

/// <summary>
/// Модель создания новой статистики устройства.
/// </summary>
/// <param name="Id">Идентификатор устройства.</param>
/// <param name="DeviceType">Тип устройства.</param>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="ClientVersion">Версия клиента.</param>
public sealed record CreateOrUpdateDeviceStatisticCommand
(
    Guid Id,
    DeviceType DeviceType,
    string? UserName,
    string? ClientVersion
) : ICommand;
