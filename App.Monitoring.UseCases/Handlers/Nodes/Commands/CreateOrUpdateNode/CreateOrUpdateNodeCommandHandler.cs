using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateOrUpdateNode;

/// <summary>
/// Обработчик команды создания/обновления узла.
/// </summary>
internal sealed class CreateOrUpdateNodeCommandHandler : ICommandHandler<CreateOrUpdateNodeCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="unitOfWorkFactory">Фабрика unit of work.</param>
    public CreateOrUpdateNodeCommandHandler(IUnitOfWorkFactory unitOfWorkFactory) => _unitOfWorkFactory = unitOfWorkFactory;

    /// <inheritdoc/>
    public async Task Handle(CreateOrUpdateNodeCommand request, CancellationToken cancellationToken)
    {
        await using var unitOfWork = _unitOfWorkFactory.Create();
        var node = new NodeEntity(request.Id, request.DeviceType, request.UserName, request.ClientVersion, DateTimeOffset.UtcNow);

        var existingNode = await unitOfWork.NodesRepository.GetAsync(request.Id, cancellationToken);

        if (existingNode is null)
        {
            await unitOfWork.NodesRepository.CreateAsync(node, cancellationToken);
        }
        else
        {
            await unitOfWork.NodesRepository.UpdateAsync(node, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
