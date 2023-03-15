using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace App.Monitoring.DataAccess.InMemory;

/// <summary>
/// Методы расширения startup.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление in-memory data access.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    public static void AddDataAccessInMemory(this IServiceCollection services) =>
        services.AddScoped<INodesRepository, NodesRepository>();
}
