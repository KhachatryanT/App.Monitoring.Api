using MediatR;

namespace App.Monitoring.UseCases;

/// <summary>
/// Интерфейс для обработчиков запросов.
/// </summary>
/// <typeparam name="TQuery">Тип объекта запроса.</typeparam>
/// <typeparam name="TResult">Тип объекта результата.</typeparam>
internal interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
