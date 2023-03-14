using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.DataAccess.Dapper.Postgresql.Entities;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Dapper;
using Mapster;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Репозиторий статистики устройств.
/// </summary>
internal sealed class DevicesStatisticsRepository : IDevicesStatisticsRepository
{
    private readonly NpgsqlConnection _connection;

    /// <summary>
    /// Контроллер <see cref="DevicesStatisticsRepository"/>.
    /// </summary>
    /// <param name="connection">Подключение к postgres.</param>
    public DevicesStatisticsRepository(NpgsqlConnection connection) => _connection = connection;

    /// <inheritdoc />
    public async IAsyncEnumerable<DeviceStatistic> GetDevicesStatisticsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"select * from {nameof(DeviceStatisticEntity)}", cancellationToken: cancellationToken);
        var deviceStatistics = await _connection.QueryAsync<DeviceStatisticEntity>(command);
        foreach (var deviceStatistic in deviceStatistics)
        {
            yield return deviceStatistic.Adapt<DeviceStatistic>();
        }
    }

    /// <inheritdoc />
    public async Task<DeviceStatistic?> GetDeviceStatisticOrDefaultAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"select * from {nameof(DeviceStatisticEntity)} where {nameof(DeviceStatisticEntity.Id)} = @id",
            parameters: new { id },
            cancellationToken: cancellationToken);
        var dto = await _connection.QuerySingleOrDefaultAsync<DeviceStatisticEntity>(command);
        return dto?.Adapt<DeviceStatistic>();
    }

    /// <inheritdoc />
    public async Task CreateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"insert into {nameof(DeviceStatisticEntity)}
    ({nameof(DeviceStatisticEntity.Id)},
     {nameof(DeviceStatisticEntity.UserName)},
     {nameof(DeviceStatisticEntity.DeviceType)},
     {nameof(DeviceStatisticEntity.StatisticDate)},
     {nameof(DeviceStatisticEntity.ClientVersion)}) values (@Id, @UserName, @DeviceType, @StatisticDate, @ClientVersion)",
            parameters: new
            {
                deviceStatistic.Id,
                deviceStatistic.UserName,
                deviceStatistic.DeviceType,
                deviceStatistic.StatisticDate,
                deviceStatistic.ClientVersion,
            },
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }

    /// <inheritdoc />
    public async Task UpdateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"update {nameof(DeviceStatisticEntity)} SET
                            {nameof(DeviceStatisticEntity.UserName)} = @UserName,
                            {nameof(DeviceStatisticEntity.DeviceType)} = @DeviceType,
                            {nameof(DeviceStatisticEntity.StatisticDate)} = @StatisticDate,
                            {nameof(DeviceStatisticEntity.ClientVersion)} = @ClientVersion
                            where {nameof(DeviceStatisticEntity.Id)} = @Id",
            parameters: new
            {
                deviceStatistic.Id,
                deviceStatistic.UserName,
                deviceStatistic.DeviceType,
                deviceStatistic.StatisticDate,
                deviceStatistic.ClientVersion,
            },
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }
}
