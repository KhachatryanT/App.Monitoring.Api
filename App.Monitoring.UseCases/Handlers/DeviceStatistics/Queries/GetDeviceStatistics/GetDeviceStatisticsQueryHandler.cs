using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;

/// <summary>
/// Обработчик запроса получения узлов.
/// </summary>
internal sealed class GetDeviceStatisticsQueryHandler : IQueryHandler<GetDeviceStatisticsQuery, IAsyncEnumerable<Node>>
{
    private readonly IMonitoringRepository _repository;

    /// <summary>
    /// <see cref="GetDeviceStatisticsQueryHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий данных.</param>
    public GetDeviceStatisticsQueryHandler(IMonitoringRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public Task<IAsyncEnumerable<Node>> Handle(GetDeviceStatisticsQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(_repository.GetNodesAsync(cancellationToken));
}
