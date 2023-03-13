using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Статистика устройства.
/// </summary>
/// <param name="Id">Идентификатор устройства.</param>
/// <param name="DeviceType">Тип устройства.</param>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="StatisticDate">Дата статистики.</param>
/// <param name="ClientVersion">Версия клиента.</param>
public sealed record DeviceStatisticResult(Guid Id, DeviceType DeviceType, string? UserName, DateTimeOffset StatisticDate, string? ClientVersion);
