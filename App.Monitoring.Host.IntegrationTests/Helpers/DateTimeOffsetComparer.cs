using System;

namespace App.Monitoring.Host.IntegrationTests.Helpers;

/// <summary>
/// Методы сравнения дат.
/// </summary>
internal static class DateTimeOffsetComparer
{
    /// <summary>
    /// Сравнение дат типа <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="first">Первая дата.</param>
    /// <param name="second">Вторая дата.</param>
    /// <returns>Результат сравнения.</returns>
    public static bool Equals(DateTimeOffset? first, DateTimeOffset? second)
    {
        if (!first.HasValue || !second.HasValue)
        {
            return false;
        }

        var firstUtc = first.Value.ToUniversalTime();
        var secondUtc = second.Value.ToUniversalTime();

        return firstUtc.Year == secondUtc.Year &&
            firstUtc.Month == secondUtc.Month &&
            firstUtc.Day == secondUtc.Day &&
            firstUtc.Hour == secondUtc.Hour &&
            firstUtc.Minute == secondUtc.Minute &&
            firstUtc.Second == secondUtc.Second;
    }
}
