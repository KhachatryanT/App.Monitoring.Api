using MediatR;

namespace App.Monitoring.UseCases;


/// <summary>
/// Интерфейс для обработчиков команд, возвращающих данные.
/// </summary>
/// <typeparam name="TCommand">Тип объекта запроса.</typeparam>
/// <typeparam name="TResult">Тип объекта результата.</typeparam>
internal interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
{
}
