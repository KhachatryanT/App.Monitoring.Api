using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Commands.CreateOrUpdateDeviceStatistic;

/// <summary>
/// Обработчик команды создания статистики устройства.
/// </summary>
internal sealed class CreateOrUpdateDeviceStatisticCommandHandler : ICommandHandler<CreateOrUpdateDeviceStatisticCommand>
{
    private readonly IDevicesStatisticsRepository _repository;

    /// <summary>
    /// <see cref="CreateOrUpdateDeviceStatisticCommandHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий статистики устройств.</param>
    public CreateOrUpdateDeviceStatisticCommandHandler(IDevicesStatisticsRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public async Task Handle(CreateOrUpdateDeviceStatisticCommand request, CancellationToken cancellationToken)
    {
        var deviceStatistic = new DeviceStatistic(request.Id, request.DeviceType, request.UserName, request.ClientVersion, DateTimeOffset.UtcNow);

        var existingDeviceStatistic = await _repository.GetDeviceStatisticOrDefaultAsync(request.Id, cancellationToken);

        if (existingDeviceStatistic is null)
        {
            await _repository.CreateDeviceStatisticAsync(deviceStatistic, cancellationToken);
        }
        else
        {
            await _repository.UpdateDeviceStatisticAsync(deviceStatistic, existingDeviceStatistic, cancellationToken);
        }
    }
}
