using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Interfaces;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Фабрика Unit of work.
/// </summary>
internal sealed class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly NpgsqlConnection _connection;
    private readonly IAppObserver<NodeEntity> _nodeModifyObserver;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgresql.</param>
    /// <param name="nodeModifyObserver">Наблюдатель изменения узлов.</param>
    public UnitOfWorkFactory(NpgsqlConnection connection, IAppObserver<NodeEntity> nodeModifyObserver)
    {
        _connection = connection;
        _nodeModifyObserver = nodeModifyObserver;
    }

    /// <inheritdoc/>
    public IUnitOfWork Create()
    {
        var connection = new NpgsqlConnection(_connection.ConnectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();
        return new UnitOfWork(transaction, _nodeModifyObserver);
    }
}
