using System;

namespace App.Monitoring.Entities.Entities;

/// <summary>
/// События узла.
/// </summary>
/// <param name="NodeId">Идентификатор узлы.</param>
/// <param name="Name">Наименование события.</param>
/// <param name="Date">Дата события.</param>
public sealed record NodeEventEntity(Guid NodeId, string? Name, DateTimeOffset? Date)
{
    /// <summary>
    /// Инициализация типа <see cref="NodeEventEntity"/>.
    /// </summary>
    [Obsolete("Вызывается десериализаторами. Используйте конструктор со всеми полями.", true)]
    public NodeEventEntity()
        : this(default, default, default)
    {
    }
}
