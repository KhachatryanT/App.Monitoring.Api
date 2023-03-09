﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistic;

/// <summary>
/// Обработчик запроса получения узлов.
/// </summary>
internal sealed class GetDeviceStatisticQueryHandler : IQueryHandler<GetDeviceStatisticQuery, DeviceStatistic>
{
    private readonly IMonitoringRepository _repository;

    /// <summary>
    /// <see cref="GetDeviceStatisticQueryHandler"/>.
    /// </summary>
    /// <param name="repository">Репозиторий данных.</param>
    public GetDeviceStatisticQueryHandler(IMonitoringRepository repository) => _repository = repository;

    /// <inheritdoc/>
    public Task<DeviceStatistic> Handle(GetDeviceStatisticQuery request, CancellationToken cancellationToken) =>
        _repository.GetDeviceStatisticAsync(request.DeviceId, cancellationToken);
}
