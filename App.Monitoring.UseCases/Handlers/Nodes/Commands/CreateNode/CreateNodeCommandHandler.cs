using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.Nodes.Commands.CreateNode;

/// <summary>
/// Обработчик команды создания узла.
/// </summary>
// ReSharper disable once UnusedType.Global
internal sealed class CreateNodeCommandHandler: ICommandHandler<CreateNodeCommand>
{
    private readonly IMonitoringRepository _repository;

    /// <summary>
    /// <see cref="CreateNodeCommandHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий данных.</param>
    public CreateNodeCommandHandler(IMonitoringRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public async Task Handle(CreateNodeCommand request, CancellationToken cancellationToken)
    {
        var node = new Node(request.DeviceId, request.DeviceType, request.Name, request.ClientVersion);
        node.DefineDate(DateTimeOffset.UtcNow);

        await _repository.CreateNodeAsync(node, cancellationToken);
    }
}
