using System.Threading.Tasks;

namespace App.Monitoring.Infrastructure.Interfaces;

/// <summary>
/// Наблюдатель завершения unit of work.
/// </summary>
public interface IUnitOfWorkCompletedObserver
{
    /// <summary>
    /// Уведомить о завершении.
    /// </summary>
    /// <returns>Task.</returns>
    Task Next();
}
