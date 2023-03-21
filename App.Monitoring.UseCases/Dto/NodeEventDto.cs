using System;

namespace App.Monitoring.UseCases.Dto;

/// <summary>
/// Событие узла.
/// </summary>
/// <param name="Name">Наименование.</param>
/// <param name="Date">Дата события.</param>
public sealed record NodeEventDto(string? Name, DateTimeOffset? Date)
{
    /// <summary>
    /// Инициализация типа.
    /// </summary>
    /// <remarks>
    /// Вызывается десериализаторами. Используйте конструктор со всеми полями.
    /// </remarks>
    private NodeEventDto()
        : this(default, default)
    {
    }
}
