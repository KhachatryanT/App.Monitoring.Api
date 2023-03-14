using System;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Методы расширения startup.
/// </summary>
public static class StartupSetup
{
    /// <summary>
    /// Добавление dapper postgresql провайдера данных.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="connectionString">Строка подключения к БД.</param>
    public static void AddDataAccessDapperPostgres(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));
        services.AddScoped<IDevicesStatisticsRepository, DevicesStatisticsRepository>();
    }

    /// <summary>
    /// Добавление сервисов для выполнения миграции БД.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="connectionString">Строка подключения к БД.</param>
    public static void AddDataAccessDapperPostgresMigrator(this IServiceCollection services, string connectionString)
    {
        var loggerProvider = services.BuildServiceProvider().GetRequiredService<ILoggerProvider>();

        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ConfigureGlobalProcessorOptions(o => o.ProviderSwitches = "Force Quote=false")
                .ScanIn(typeof(StartupSetup).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddProvider(loggerProvider));
    }

    /// <summary>
    /// Миграции БД.
    /// </summary>
    /// <param name="services"><see cref="IServiceProvider"/>.</param>
    public static void MigrateDatabase(this IServiceProvider services)
    {
        using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}
