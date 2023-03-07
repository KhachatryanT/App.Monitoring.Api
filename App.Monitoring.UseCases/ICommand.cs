using MediatR;

namespace App.Monitoring.UseCases;

/// <summary>
/// Интерфейс для запросов, меняющих состояние.
/// </summary>
internal interface ICommand : IRequest
{
}
