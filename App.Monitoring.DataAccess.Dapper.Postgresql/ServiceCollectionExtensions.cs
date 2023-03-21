using System;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace App.Monitoring.DataAccess.Dapper.Postgresql;

/// <summary>
/// Методы расширения IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление dapper postgresql провайдера данных.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="connectionString">Строка подключения к БД.</param>
    public static void AddDataAccessDapperPostgresql(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));
        services.AddScoped<INodesRepository, NodesRepository>();
        services.AddScoped<INodeEventsRepository, NodeEventsRepository>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
        SqlMapper.AddTypeHandler(new DateTimeOffsetToUtcHandler());
    }

    /// <summary>
    /// Добавление сервисов для выполнения миграции БД.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="connectionString">Строка подключения к БД.</param>
    public static void AddDataAccessDapperPostgresqlMigrator(this IServiceCollection services, string connectionString)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var loggerProvider = services.BuildServiceProvider().GetRequiredService<ILoggerProvider>();

        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ConfigureGlobalProcessorOptions(o => o.ProviderSwitches = "Force Quote=false")
                .ScanIn(typeof(ApplicationBuilderExtensions).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddProvider(loggerProvider));
    }
}
