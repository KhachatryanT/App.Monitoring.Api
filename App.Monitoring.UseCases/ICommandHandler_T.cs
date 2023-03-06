using MediatR;

namespace App.Monitoring.UseCases;

internal interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
{
}
