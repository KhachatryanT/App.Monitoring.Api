using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace App.Monitoring.UseCases;

/// <summary>
/// Методы расширения startup
/// </summary>
public static class StartupSetup
{
	/// <summary>
	/// Добавление Use cases
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddUseCases(this IServiceCollection services)
	{
		var assembly = typeof(StartupSetup).Assembly;
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
		return services;
	}
}
