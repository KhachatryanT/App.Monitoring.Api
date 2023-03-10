using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Entities.Models;

/// <summary>
/// Устройство.
/// </summary>
/// <param name="Id">Идентификатор устройства.</param>
/// <param name="DeviceType">Тип устройства.</param>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="ClientVersion">Версия клиента.</param>
/// <param name="StatisticDate">Дата статистики.</param>
public sealed record DeviceStatistic(Guid Id, DeviceType DeviceType, string? UserName, string? ClientVersion, DateTimeOffset StatisticDate);
