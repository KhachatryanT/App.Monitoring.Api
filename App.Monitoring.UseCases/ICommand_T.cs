using MediatR;

namespace App.Monitoring.UseCases;

/// <summary>
/// Интерфейс для запросов, меняющих состояние.
/// </summary>
/// <typeparam name="T">Тип объекта результата.</typeparam>
internal interface ICommand<out T> : IRequest<T>
{

}
