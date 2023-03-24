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
internal sealed class NodeEventsRepository : INodeEventsRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction? _transaction;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgresql.</param>
    [ActivatorUtilitiesConstructor]
    public NodeEventsRepository(IDbConnection connection) => _connection = connection;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="transaction">Транзакция БД.</param>
    public NodeEventsRepository(IDbTransaction transaction)
    {
        _transaction = transaction;
        _connection = transaction.Connection ?? throw new ArgumentNullException(nameof(transaction.Connection));
    }

    /// <inheritdoc/>
    public async Task CreateAsync(IEnumerable<NodeEventEntity> events, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"INSERT INTO node_events (node_id, name, date) VALUES
                                         (@{nameof(NodeEventEntity.NodeId)},
                                         @{nameof(NodeEventEntity.Name)},
                                         @{nameof(NodeEventEntity.Date)})",
            events,
            _transaction,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<NodeEventEntity>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"SELECT * FROM node_events WHERE node_id = @{nameof(nodeId)}",
            new { nodeId },
            _transaction,
            cancellationToken: cancellationToken);
        return await _connection.QueryAsync<NodeEventEntity>(command);
    }
}
