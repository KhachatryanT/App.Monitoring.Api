using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Monitoring.Host.IntegrationTests.Helpers;

/// <summary>
/// Методы расширения <see cref="HttpResponseMessage"/>.
/// </summary>
internal static class HttpResponseMessageExtensions
{
    private static readonly JsonSerializerOptions serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    /// <summary>
    /// Десериализация объекта в тип.
    /// </summary>
    /// <param name="response">Ответ на запрос.</param>
    /// <typeparam name="T">Тип для десериализации.</typeparam>
    /// <returns>Десериализованный объект.</returns>
    /// <exception cref="SerializationException">Десериализация невозможна.</exception>
    public static async Task<T> DeserializeResponseAsync<T>(this HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(body, serializerOptions) ?? throw new SerializationException();
    }
}
