using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Фабрика Unit of work.
/// </summary>
internal sealed class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly NpgsqlConnection _connection;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgresql.</param>
    public UnitOfWorkFactory(NpgsqlConnection connection) => _connection = connection;

    /// <inheritdoc/>
    public IUnitOfWork Create()
    {
        var connection = new NpgsqlConnection(_connection.ConnectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();
        return new UnitOfWork(transaction);
    }
}
