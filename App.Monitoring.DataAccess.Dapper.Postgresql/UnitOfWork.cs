using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Транзакционная работа с репозиториями.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction _transaction;
    private INodeEventsRepository? _nodeEventsRepository;
    private INodesRepository? _nodesRepository;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connectionString">Подключение к postgresql.</param>
    public UnitOfWork(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
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

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
        await _connection.DisposeAsync();
    }
}
