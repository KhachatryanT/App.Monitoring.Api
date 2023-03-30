using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <inheritdoc/>
internal sealed class NodesRepository : INodesRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction? _transaction;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgresql.</param>
    [ActivatorUtilitiesConstructor]
    public NodesRepository(IDbConnection connection) => _connection = connection;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="transaction">Транзакция БД.</param>
    /// <exception cref="ArgumentNullException">Коннекция отсутствует.</exception>
    public NodesRepository(IDbTransaction transaction)
        : this(transaction.Connection ?? throw new ArgumentNullException(nameof(transaction.Connection))) =>
        _transaction = transaction;

    /// <inheritdoc/>
    public async Task CreateAsync(NodeEntity nodeEntity, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"INSERT INTO nodes (id, user_name, device_type, statistic_date, client_version) VALUES
                                        (@{nameof(nodeEntity.Id)},
                                         @{nameof(nodeEntity.UserName)},
                                         @{nameof(nodeEntity.DeviceType)},
                                         @{nameof(nodeEntity.StatisticDate)},
                                         @{nameof(nodeEntity.ClientVersion)})",
            nodeEntity,
            _transaction,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<NodeEntity>> GetAsync(CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@"SELECT * FROM nodes",
            transaction: _transaction,
            cancellationToken: cancellationToken);
        return await _connection.QueryAsync<NodeEntity>(command);
    }

    /// <inheritdoc/>
    public async Task<NodeEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"SELECT * FROM nodes WHERE id = @{nameof(id)}",
            new { id },
            _transaction,
            cancellationToken: cancellationToken);
        return await _connection.QuerySingleOrDefaultAsync<NodeEntity>(command);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(NodeEntity nodeEntity, CancellationToken cancellationToken = default)
    {
        var command = new CommandDefinition(@$"UPDATE nodes SET
                            user_name = @{nameof(nodeEntity.UserName)},
                            device_type = @{nameof(nodeEntity.DeviceType)},
                            statistic_date = @{nameof(nodeEntity.StatisticDate)},
                            client_version = @{nameof(nodeEntity.ClientVersion)}
                            where id = @{nameof(nodeEntity.Id)}",
            nodeEntity,
            _transaction,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }
}
