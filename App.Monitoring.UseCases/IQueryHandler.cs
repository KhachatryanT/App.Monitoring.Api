using MediatR;

namespace App.Monitoring.UseCases;

internal interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
	where TQuery : IQuery<TResult>
{
}
