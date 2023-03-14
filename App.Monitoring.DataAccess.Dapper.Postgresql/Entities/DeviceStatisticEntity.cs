using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.DataAccess.Dapper.Postgresql.Entities;

/// <summary>
/// Статистика устройства.
/// </summary>
/// <param name="Id">Идентификатор устройства.</param>
/// <param name="DeviceType">Тип устройства.</param>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="ClientVersion">Версия клиента.</param>
/// <param name="StatisticDate">Дата статистики.</param>
public sealed record DeviceStatisticEntity(Guid Id, DeviceType DeviceType, string? UserName, string? ClientVersion, DateTime StatisticDate)
{
    /// <summary>
    /// Инициализация со свойствами по-умолчанию.
    /// </summary>
    /// <remarks>
    /// Необходим для десериализаторов.
    /// </remarks>
    public DeviceStatisticEntity()
        : this(default, default, default, default, default)
    {
    }
}
