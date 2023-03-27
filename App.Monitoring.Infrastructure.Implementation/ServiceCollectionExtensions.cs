using App.Monitoring.Entities.Entities;
using App.Monitoring.Infrastructure.Implementation.Observers;
using App.Monitoring.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace App.Monitoring.Infrastructure.Implementation;

/// <summary>
/// Методы расширения IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление dapper postgresql провайдера данных.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    public static void AddNodesToNotificationHubEmitterObserver(this IServiceCollection services) => services.AddScoped<IAppObserver<NodeEntity>, NodesToNotificationHubEmitterObserver>();
}
