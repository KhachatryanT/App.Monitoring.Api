using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Узел.
/// </summary>
/// <param name="Id">Идентификатор устройства.</param>
/// <param name="Os">Тип устройства.</param>
/// <param name="Name">Имя пользователя.</param>
/// <param name="Version">Версия клиента.</param>
public sealed record Node(Guid Id, DeviceType Os, string? Name, string? Version);
