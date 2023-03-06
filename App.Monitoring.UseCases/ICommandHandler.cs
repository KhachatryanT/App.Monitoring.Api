using MediatR;

namespace App.Monitoring.UseCases;

internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
	where TCommand : ICommand
{
}
