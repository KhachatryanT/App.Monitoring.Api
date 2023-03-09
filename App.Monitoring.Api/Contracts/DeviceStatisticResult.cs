using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Статистика устройства.
/// </summary>
public class DeviceStatisticResult
{
    /// <summary>
    /// Идентификатор устройства.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Тип устройства.
    /// </summary>
    public DeviceType DeviceType { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Дата статистики.
    /// </summary>
    public DateTimeOffset StatisticDate { get; set; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    public string? ClientVersion { get; set; }
}
