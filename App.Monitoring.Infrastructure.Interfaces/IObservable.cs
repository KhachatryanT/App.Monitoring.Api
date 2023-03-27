using System.Threading.Tasks;

namespace App.Monitoring.Infrastructure.Interfaces;

/// <summary>
/// Наблюдатель.
/// </summary>
/// <typeparam name="T">Тип передаваемого события.</typeparam>
public interface IAppObserver<in T>
    where T : class
{
    /// <summary>
    /// Уведомить о событии.
    /// </summary>
    /// <param name="obj">Объект уведомления.</param>
    /// <returns>Task.</returns>
    Task Next(T obj);
}
