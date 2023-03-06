﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Monitoring.Host.Application;

/// <summary>
/// Методы расширения EF миграций
/// </summary>
public static class MigrationExtensions
{
	/// <summary>
	/// Применить имеющиеся миграции
	/// </summary>
	/// <param name="app"></param>
	/// <typeparam name="T"></typeparam>
	public static async Task EnsureMigrationAsync<T>(this IApplicationBuilder app)
		where T : DbContext
	{
		var hostApplicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
		using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

		var context = serviceScope.ServiceProvider.GetRequiredService<T>();
        if (context.Database.IsRelational())
        {
            await context.Database.MigrateAsync(hostApplicationLifetime.ApplicationStopping);
        }
    }
}
