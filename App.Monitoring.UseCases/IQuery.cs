using MediatR;

namespace App.Monitoring.UseCases;

/// <summary>
/// Интерфейс для идемпотентных запросов.
/// </summary>
/// <typeparam name="T">Тип модели результата.</typeparam>
internal interface IQuery<out T> : IRequest<T>
{
}
