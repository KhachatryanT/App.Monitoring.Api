using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace App.Monitoring.DataAccess.InMemory;

/// <summary>
/// InMemory контекст БД.
/// </summary>
public sealed class AppDbContext: DbContext, IDbContext
{
    /// <summary>
    /// <see cref="AppDbContext"/>.
    /// </summary>
    /// <param name="options"><see cref="DbContextOptions"/>.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Узлы.
    /// </summary>
    public DbSet<Node> Nodes { get; init; } = default!;
}
