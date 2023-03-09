using System.Collections.Generic;
using System.Threading;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;

/// <summary>
/// Обработчик запроса получения узлов.
/// </summary>
internal sealed class GetDeviceStatisticsQueryHandler : IStreamQueryHandler<GetDeviceStatisticsQuery, DeviceStatistic>
{
    private readonly IMonitoringRepository _repository;

    /// <summary>
    /// <see cref="GetDeviceStatisticsQueryHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий данных.</param>
    public GetDeviceStatisticsQueryHandler(IMonitoringRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public IAsyncEnumerable<DeviceStatistic> Handle(GetDeviceStatisticsQuery request, CancellationToken cancellationToken) =>
        _repository.GetDevicesStatisticsAsync(cancellationToken);
}
