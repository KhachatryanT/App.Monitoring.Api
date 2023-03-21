using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <inheritdoc/>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction _transaction;
    private INodeEventsRepository? _nodeEventsRepository;
    private INodesRepository? _nodesRepository;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgresql.</param>
    public UnitOfWork(NpgsqlConnection connection)
    {
        _connection = connection;
        _transaction = _connection.BeginTransaction();
    }

    /// <inheritdoc/>
    public INodeEventsRepository NodeEventsRepository => _nodeEventsRepository ??= new NodeEventsRepository(_connection);

    /// <inheritdoc/>
    public INodesRepository NodesRepository => _nodesRepository ??= new NodesRepository(_connection);

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
