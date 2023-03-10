using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Модель создания новой статистики устройства.
/// </summary>
/// <param name="DeviceType">Тип устройства.</param>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="ClientVersion">Версия клиента.</param>
public sealed record CreateDeviceStatisticRequest(DeviceType DeviceType, string? UserName, string? ClientVersion);
