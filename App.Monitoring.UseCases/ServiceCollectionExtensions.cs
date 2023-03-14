using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace App.Monitoring.UseCases;

/// <summary>
/// Методы расширения startup.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление use cases по работе со статистикой.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    public static void AddDeviceStatisticsUseCases(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
    }
}
