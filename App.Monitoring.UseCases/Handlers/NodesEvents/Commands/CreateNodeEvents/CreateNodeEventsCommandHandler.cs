using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Entities.Enums;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.NodesEvents.Commands.CreateNodeEvents;

/// <summary>
/// Обработчик команды создания новых событий узла.
/// </summary>
internal sealed class CreateNodeEventsCommandHandler : ICommandHandler<CreateNodeEventsCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="unitOfWorkFactory">Фабрика unit of work.</param>
    public CreateNodeEventsCommandHandler(IUnitOfWorkFactory unitOfWorkFactory) => _unitOfWorkFactory = unitOfWorkFactory;

    /// <inheritdoc/>
    public async Task Handle(CreateNodeEventsCommand request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Create();
        var node = await unitOfWork.NodesRepository.GetAsync(request.NodeId, cancellationToken);
        if (node is null)
        {
            node = new NodeEntity(request.NodeId, DeviceType.Unknown, default, default, default);
            await unitOfWork.NodesRepository.CreateAsync(node, cancellationToken);
        }

        var eventsEntities = request.Events.Select(x => new NodeEventEntity(request.NodeId, x.Name, x.Date));
        await unitOfWork.NodeEventsRepository.CreateAsync(eventsEntities, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
