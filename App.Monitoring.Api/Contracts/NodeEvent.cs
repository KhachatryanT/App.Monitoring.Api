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
    [Obsolete("Вызывается десериализаторами. Используйте конструктор со всеми полями.", true)]
    public NodeEvent()
        : this(default, default)
    {
    }
}
