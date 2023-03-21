using System;
using App.Monitoring.Entities.Enums;

namespace App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateOrUpdateNode;

/// <summary>
/// Модель команды создания нового узла.
/// </summary>
/// <param name="Id">Идентификатор устройства.</param>
/// <param name="DeviceType">Тип устройства.</param>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="ClientVersion">Версия клиента.</param>
public sealed record CreateOrUpdateNodeCommand(Guid Id, DeviceType DeviceType, string? UserName, string? ClientVersion) : ICommand;
