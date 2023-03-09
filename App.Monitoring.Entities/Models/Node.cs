using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.Entities.Models;

/// <summary>
/// Узел.
/// </summary>
public sealed record Node
{
    /// <summary>
    /// Конструктор по-умолчанию для сериализаторов.
    /// </summary>
    public Node()
    {
        UserName = string.Empty;
        ClientVersion = string.Empty;
    }

    /// <summary>
    /// <see cref="Node"/>.
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства.</param>
    /// <param name="deviceType">Тип устройства.</param>
    /// <param name="userName">Имя пользователя.</param>
    /// <param name="clientVersion">Версия клиента.</param>
    public Node(Guid deviceId, DeviceType deviceType, string userName, string clientVersion)
    {
        DeviceId = deviceId;
        DeviceType = deviceType;
        UserName = userName;
        ClientVersion = clientVersion;
    }

    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public int Id { get; private set; }

    /// <summary>
    /// Идентификатор устройства.
    /// </summary>
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public Guid DeviceId { get; private set; }

    /// <summary>
    /// Тип устройства.
    /// </summary>
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public DeviceType DeviceType { get; private set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public string UserName { get; private set; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public string ClientVersion { get; private set; }

    /// <summary>
    /// Дата статистики.
    /// </summary>
    public DateTimeOffset Date { get; private set; }

    /// <summary>
    /// Установить дату статистики.
    /// </summary>
    /// <param name="date">Дата статистики.</param>
    public void DefineDate(DateTimeOffset date) => Date = date;
}
