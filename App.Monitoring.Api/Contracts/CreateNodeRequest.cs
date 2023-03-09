using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Utils;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Модель создания нового узла.
/// </summary>
public sealed record CreateNodeRequest : IValidatableObject
{
    /// <summary>
    /// Конструктор по-умолчанию для десериализации.
    /// </summary>
    public CreateNodeRequest()
    {
        Name = string.Empty;
        ClientVersion = string.Empty;
    }

    /// <summary>
    /// Идентификатор устройства.
    /// </summary>
    public Guid DeviceId { get; init; }

    /// <summary>
    /// Тип устройства.
    /// </summary>
    public DeviceType DeviceType { get; init; }

    /// <summary>
    /// Имя узла.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "NodeNameNotSpecified")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public string Name { get; init; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ClientVersionNotSpecified")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public string ClientVersion { get; init; }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (DeviceId == default)
        {
            results.Add(new ValidationResult(ErrorMessages.DeviceIdNotSpecified, new[] { nameof(DeviceId) }));
        }

        if (DeviceType == default)
        {
            results.Add(new ValidationResult(ErrorMessages.DeviceTypeNotSpecified, new[] { nameof(DeviceType) }));
        }

        return results;
    }
}
