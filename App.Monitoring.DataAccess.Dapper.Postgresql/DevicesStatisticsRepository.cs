using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Dapper;
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
    public async Task<IEnumerable<DeviceStatistic>> GetDevicesStatisticsAsync(CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"select * from Device_Statistics", cancellationToken: cancellationToken);
        return await _connection.QueryAsync<DeviceStatistic>(command);
    }

    /// <inheritdoc />
    public async Task<DeviceStatistic?> GetDeviceStatisticOrDefaultAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"select * from Device_Statistics where Id = @id",
            parameters: new { id },
            cancellationToken: cancellationToken);
        return await _connection.QuerySingleOrDefaultAsync<DeviceStatistic>(command);
    }

    /// <inheritdoc />
    public async Task CreateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"insert into Device_Statistics (Id, User_Name, Device_Type, Statistic_Date, Client_Version) values
                                        (@{nameof(deviceStatistic.Id)},
                                         @{nameof(deviceStatistic.UserName)},
                                         @{nameof(deviceStatistic.DeviceType)},
                                         @{nameof(deviceStatistic.StatisticDate)},
                                         @{nameof(deviceStatistic.ClientVersion)})",
            parameters: deviceStatistic,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }

    /// <inheritdoc />
    public async Task UpdateDeviceStatisticAsync(DeviceStatistic deviceStatistic, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"update Device_Statistics SET
                            User_Name = @{nameof(deviceStatistic.UserName)},
                            Device_Type = @{nameof(deviceStatistic.DeviceType)},
                            Statistic_Date = @{nameof(deviceStatistic.StatisticDate)},
                            Client_Version = @{nameof(deviceStatistic.ClientVersion)}
                            where Id = @{nameof(deviceStatistic.Id)}",
            parameters: deviceStatistic,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }
}
