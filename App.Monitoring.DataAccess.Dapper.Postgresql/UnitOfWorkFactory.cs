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
    private readonly IUnitOfWorkCompletedObserver _unitOfWorkCompletedObserver;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="connection">Подключение к postgresql.</param>
    /// <param name="unitOfWorkCompletedObserver">Наблюдатель завершения unit of work.</param>
    public UnitOfWorkFactory(NpgsqlConnection connection, IUnitOfWorkCompletedObserver unitOfWorkCompletedObserver)
    {
        _connection = connection;
        _unitOfWorkCompletedObserver = unitOfWorkCompletedObserver;
    }

    /// <inheritdoc/>
    public IUnitOfWork Create()
    {
        var connection = new NpgsqlConnection(_connection.ConnectionString);
        connection.Open();
        var unitOfWork = new UnitOfWork(connection.BeginTransaction());
        unitOfWork.Subscribe(_unitOfWorkCompletedObserver);
        return unitOfWork;
    }
}
