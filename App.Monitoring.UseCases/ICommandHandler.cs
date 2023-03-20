using MediatR;

namespace App.Monitoring.UseCases;

/// <summary>
/// Интерфейс для обработчиков команд без возвращаемых данных.
/// </summary>
/// <typeparam name="TCommand">Тип объекта запроса.</typeparam>
internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}
