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
        Name = string.Empty;
        ClientVersion = string.Empty;
    }

    /// <summary>
    /// <see cref="Node"/>.
    /// </summary>
    /// <param name="deviceId"><see cref="DeviceId"/>.</param>
    /// <param name="deviceType"><see cref="DeviceType"/>.</param>
    /// <param name="name"><see cref="Name"/>.</param>
    /// <param name="clientVersion"><see cref="ClientVersion"/>.</param>
    public Node(Guid deviceId, DeviceType deviceType, string name, string clientVersion)
    {
        DeviceId = deviceId;
        DeviceType = deviceType;
        Name = name;
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
    /// Имя узла.
    /// </summary>
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public string Name { get; private set; }

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
