using System.Collections.Generic;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;

/// <summary>
/// Запрос получения узлов.
/// </summary>
public sealed class GetDeviceStatisticsQuery : IQuery<IAsyncEnumerable<Node>>
{
}
