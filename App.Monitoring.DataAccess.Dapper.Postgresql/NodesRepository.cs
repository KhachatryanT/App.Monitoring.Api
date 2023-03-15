using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Dapper;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <inheritdoc />
internal sealed class NodesRepository : INodesRepository
{
    private readonly NpgsqlConnection _connection;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgres.</param>
    public NodesRepository(NpgsqlConnection connection) => _connection = connection;

    /// <inheritdoc />
    public async Task<IEnumerable<NodeEntity>> GetAsync(CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"SELECT * FROM nodes", cancellationToken: cancellationToken);
        return await _connection.QueryAsync<NodeEntity>(command);
    }

    /// <inheritdoc />
    public async Task<NodeEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"SELECT * FROM nodes WHERE id = @{nameof(id)}",
            parameters: new { id },
            cancellationToken: cancellationToken);
        return await _connection.QuerySingleOrDefaultAsync<NodeEntity>(command);
    }

    /// <inheritdoc />
    public async Task InsertAsync(NodeEntity nodeEntity, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"INSERT INTO nodes (id, user_name, device_type, statistic_date, client_version) VALUES
                                        (@{nameof(nodeEntity.Id)},
                                         @{nameof(nodeEntity.UserName)},
                                         @{nameof(nodeEntity.DeviceType)},
                                         @{nameof(nodeEntity.StatisticDate)},
                                         @{nameof(nodeEntity.ClientVersion)})",
            parameters: nodeEntity,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(NodeEntity nodeEntity, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"UPDATE nodes SET
                            user_name = @{nameof(nodeEntity.UserName)},
                            device_type = @{nameof(nodeEntity.DeviceType)},
                            statistic_date = @{nameof(nodeEntity.StatisticDate)},
                            client_version = @{nameof(nodeEntity.ClientVersion)}
                            where id = @{nameof(nodeEntity.Id)}",
            parameters: nodeEntity,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }
}
