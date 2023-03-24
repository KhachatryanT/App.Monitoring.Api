using System;
using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Транзакционная работа с репозиториями.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly NpgsqlTransaction _transaction;
    private INodeEventsRepository? _nodeEventsRepository;
    private INodesRepository? _nodesRepository;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="transaction">Транзакция.</param>
    public UnitOfWork(NpgsqlTransaction transaction) => _transaction = transaction;

    /// <inheritdoc/>
    public INodeEventsRepository NodeEventsRepository => _nodeEventsRepository ??= new NodeEventsRepository(_transaction);

    /// <inheritdoc/>
    public INodesRepository NodesRepository => _nodesRepository ??= new NodesRepository(_transaction);

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
        if (_transaction.Connection != null)
        {
            await _transaction.Connection.DisposeAsync();
        }
    }
}
