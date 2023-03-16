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
    private readonly INodesRepository _nodesRepository;

    /// <summary>
    /// <see cref="CreateOrUpdateNodeCommandHandler"/>.
    /// </summary>
    /// <param name="nodesRepository">Репозиторий узлов.</param>
    public CreateOrUpdateNodeCommandHandler(INodesRepository nodesRepository) => _nodesRepository = nodesRepository;

    /// <inheritdoc/>
    public async Task Handle(CreateOrUpdateNodeCommand request, CancellationToken cancellationToken)
    {
        var node = new NodeEntity(request.Id, request.DeviceType, request.UserName, request.ClientVersion, DateTimeOffset.UtcNow);

        var existingNode = await _nodesRepository.GetAsync(request.Id, cancellationToken);

        if (existingNode is null)
        {
            await _nodesRepository.CreateAsync(node, cancellationToken);
        }
        else
        {
            await _nodesRepository.UpdateAsync(node, cancellationToken);
        }
    }
}
