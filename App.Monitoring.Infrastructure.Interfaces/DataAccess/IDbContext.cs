using System.Threading;
using System.Threading.Tasks;
using App.Monitoring.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Monitoring.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Дата контекст приложения.
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Узлы.
    /// </summary>
    public DbSet<Node> Nodes { get;}

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>.
    /// <returns>Количество записей, записанных в базу данных.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
