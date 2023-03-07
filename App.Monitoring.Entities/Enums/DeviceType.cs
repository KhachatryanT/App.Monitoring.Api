namespace App.Monitoring.Entities.Enums;

/// <summary>
/// Тип устройства.
/// </summary>
public enum DeviceType
{
    /// <summary>
    /// Не определено.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// <see cref="Android"/>.
    /// </summary>
    Android = 1,

    /// <summary>
    /// <see cref="Iphone"/>.
    /// </summary>
    Iphone = 2,

    /// <summary>
    /// <see cref="Windows"/>.
    /// </summary>
    Windows = 3,

    /// <summary>
    /// <see cref="Linux"/>.
    /// </summary>
    Linux = 4,

    /// <summary>
    /// <see cref="MacOs"/>.
    /// </summary>
    MacOs = 5,
}
