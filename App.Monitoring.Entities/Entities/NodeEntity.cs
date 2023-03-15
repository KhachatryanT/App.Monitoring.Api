using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Entities.Entities;

/// <summary>
/// Узел.
/// </summary>
/// <param name="Id">Идентификатор устройства.</param>
/// <param name="DeviceType">Тип устройства.</param>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="ClientVersion">Версия клиента.</param>
/// <param name="StatisticDate">Дата статистики.</param>
public sealed record NodeEntity(Guid Id, DeviceType DeviceType, string? UserName, string? ClientVersion, DateTimeOffset? StatisticDate)
{
    /// <summary>
    /// Инициализация типа <see cref="NodeEntity"/>.
    /// </summary>
    [Obsolete("Вызывается десериализаторами. Используйте конструктор со всеми полями. ", true)]
    public NodeEntity()
        : this(default, default, default, default, default)
    {
    }
}
