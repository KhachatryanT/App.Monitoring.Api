using MediatR;

namespace App.Monitoring.UseCases;

/// <summary>
/// Интерфейс для идемпотентных потоковых запросов.
/// </summary>
/// <typeparam name="T">Тип модели результата.</typeparam>
internal interface IStreamQuery<out T> : IStreamRequest<T>
{
}
