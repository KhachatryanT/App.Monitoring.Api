using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Dapper;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <inheritdoc/>
internal sealed class NodesEventsRepository : INodesEventsRepository
{
    private readonly NpgsqlConnection _connection;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgres.</param>
    public NodesEventsRepository(NpgsqlConnection connection) => _connection = connection;

    /// <inheritdoc/>
    public async Task CreateAsync(IEnumerable<NodeEventEntity> events, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"INSERT INTO node_events (node_id, name, date) VALUES
                                         (@{nameof(NodeEventEntity.NodeId)},
                                         @{nameof(NodeEventEntity.Name)},
                                         @{nameof(NodeEventEntity.Date)})",
            events,
            cancellationToken: cancellationToken);
        await _connection.ExecuteAsync(command);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<NodeEventEntity>> GetByNodeIdAsync(Guid nodeId, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(@$"SELECT * FROM node_events WHERE node_id = @{nameof(nodeId)}",
            new { nodeId },
            cancellationToken: cancellationToken);
        return await _connection.QueryAsync<NodeEventEntity>(command);
    }
}
