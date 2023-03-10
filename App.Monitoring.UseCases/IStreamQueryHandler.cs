using MediatR;

namespace App.Monitoring.UseCases;

/// <summary>
/// Интерфейс для обработчиков запросов с потоковым результатом.
/// </summary>
/// <typeparam name="TQuery">Тип объекта запроса.</typeparam>
/// <typeparam name="TResult">Тип объекта результата.</typeparam>
internal interface IStreamQueryHandler<in TQuery, out TResult> : IStreamRequestHandler<TQuery, TResult>
	where TQuery : IStreamQuery<TResult>
{
}
