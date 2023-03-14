using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;

/// <summary>
/// Обработчик запроса получения статистики устройств.
/// </summary>
internal sealed class GetDeviceStatisticsQueryHandler : IQueryHandler<GetDeviceStatisticsQuery, IEnumerable<DeviceStatistic>>
{
    private readonly IDevicesStatisticsRepository _repository;

    /// <summary>
    /// <see cref="GetDeviceStatisticsQueryHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий статистики устройств.</param>
    public GetDeviceStatisticsQueryHandler(IDevicesStatisticsRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public Task<IEnumerable<DeviceStatistic>> Handle(GetDeviceStatisticsQuery request, CancellationToken cancellationToken) =>
        _repository.GetDevicesStatisticsAsync(cancellationToken);
}
