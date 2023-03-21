using System;

namespace App.Monitoring.Api.Contracts;

/// <summary>
/// Событие узла.
/// </summary>
/// <param name="Name">Наименование.</param>
/// <param name="Date">Дата события.</param>
public sealed record NodeEvent(string? Name, DateTimeOffset? Date)
{
    /// <summary>
    /// Инициализация типа.
    /// </summary>
    /// <remarks>
    /// Вызывается десериализаторами. Используйте конструктор со всеми полями.
    /// </remarks>
    private NodeEvent()
        : this(default, default)
    {
    }
}
