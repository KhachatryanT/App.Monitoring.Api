namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Фабрика  unit of work.
/// </summary>
public interface IUnitOfWorkFactory
{
    /// <summary>
    /// Создание нового экземпляра unit of work.
    /// </summary>
    /// <returns>Unit of work.</returns>
    IUnitOfWork Create();
}
