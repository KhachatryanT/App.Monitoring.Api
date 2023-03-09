using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace App.Monitoring.UseCases;

/// <summary>
/// Методы расширения startup.
/// </summary>
public static class StartupSetup
{
    /// <summary>
    /// Добавление use cases по работе со статистикой
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    public static void AddDeviceStatisticsUseCases(this IServiceCollection services)
    {
        var assembly = typeof(StartupSetup).Assembly;
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
    }
}
