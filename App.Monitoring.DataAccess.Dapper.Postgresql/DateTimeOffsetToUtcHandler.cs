using System;
using System.Data;
using Dapper;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Преобразование дат во временную зону UTC.
/// </summary>
/// <remarks>
/// Postgresql не поддерживает offset, отличные от 0.
/// </remarks>
public class DateTimeOffsetToUtcHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    /// <inheritdoc/>
    public override DateTimeOffset Parse(object value)
    {
        var date = (DateTime)value;
        return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, TimeSpan.Zero);
    }

    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value) => parameter.Value = value.UtcDateTime;
}
