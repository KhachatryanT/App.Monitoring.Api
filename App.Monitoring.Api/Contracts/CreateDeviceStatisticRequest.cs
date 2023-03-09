using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Модель создания новой статистики устройства.
/// </summary>
public sealed record CreateDeviceStatisticRequest
{
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
