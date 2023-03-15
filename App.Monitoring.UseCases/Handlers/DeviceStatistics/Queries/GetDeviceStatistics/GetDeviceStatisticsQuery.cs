﻿using System.Collections.Generic;
using App.Monitoring.Entities.Models;

namespace App.Monitoring.UseCases.Handlers.DeviceStatistics.Queries.GetDeviceStatistics;

/// <summary>
/// Запрос получения статистики устройств.
/// </summary>
public sealed class GetDeviceStatisticsQuery : IQuery<IEnumerable<DeviceStatistic>>
{
}
