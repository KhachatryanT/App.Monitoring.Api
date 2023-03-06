using App.Monitoring.Entities.Models;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace App.Monitoring.DataAccess.InMemory;

/// <inheritdoc />
public sealed class AppDbContext: DbContext, IDbContext
{
    /// <inheritdoc />
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Узлы
    /// </summary>
    public DbSet<Node> Nodes { get; init; } = default!;
}
