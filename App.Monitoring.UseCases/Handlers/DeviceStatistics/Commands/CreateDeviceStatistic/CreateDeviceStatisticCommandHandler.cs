using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Commands.CreateDeviceStatistic;

/// <summary>
/// Обработчик команды создания узла.
/// </summary>
internal sealed class CreateDeviceStatisticCommandHandler: ICommandHandler<CreateDeviceStatisticCommand>
{
    private readonly IMonitoringRepository _repository;

    /// <summary>
    /// <see cref="CreateDeviceStatisticCommandHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий данных.</param>
    public CreateDeviceStatisticCommandHandler(IMonitoringRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public async Task Handle(CreateDeviceStatisticCommand request, CancellationToken cancellationToken)
    {
        var node = new Node(request.DeviceId, request.DeviceType, request.UserName, request.ClientVersion, DateTimeOffset.UtcNow);
        await _repository.CreateNodeAsync(node, cancellationToken);
    }
}
