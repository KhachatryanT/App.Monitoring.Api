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
    public string? UserName { get; init; }

    /// <summary>
    /// Версия клиента.
    /// </summary>
    public string? ClientVersion { get; init; }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (DeviceId == default)
        {
            results.Add(new ValidationResult(ErrorMessages.DeviceIdNotSpecified, new[] { nameof(DeviceId) }));
        }

        return results;
    }
}
