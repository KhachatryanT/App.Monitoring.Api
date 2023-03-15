using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace App.Monitoring.Infrastructure.Implementation.Converters;

/// <summary>
/// Формат сериализации даты.
/// </summary>
public sealed class DateTimeOffsetFormatConverter : JsonConverter<DateTimeOffset>
{
    private readonly string _format;

    /// <summary>
    /// Инифиализация.
    /// </summary>
    /// <param name="format">Формат даты.</param>
    public DateTimeOffsetFormatConverter(string format) => _format = format;

    /// <inheritdoc />
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        DateTimeOffset.Parse(reader.GetString() ?? string.Empty);

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(
            _format, CultureInfo.InvariantCulture));
}
