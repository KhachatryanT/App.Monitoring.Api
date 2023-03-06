using System;
using System.ComponentModel.DataAnnotations;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Utils;

namespace App.Monitoring.Controllers.Models;

/// <summary>
/// Модель создания нового узла
/// </summary>
public sealed class CreateNodeRequest
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "DeviceIdNotSpecified")]
    public Guid? DeviceId { get; init; }

    /// <summary>
    /// Тип устройства
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "DeviceTypeNotSpecified")]
    public DeviceType? DeviceType { get; init; }

    /// <summary>
    /// Имя узла
    /// </summary>
    [Required( AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "NodeNameNotSpecified")]
    public string? Name { get; init; }

    /// <summary>
    /// Дата последней статистики
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "LastStatisticDateNotSpecified")]
    public DateTimeOffset? Date { get; init; }

    /// <summary>
    /// Версия клиента
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ClientVersionNotSpecified")]
    public string? ClientVersion { get; init; }
}
