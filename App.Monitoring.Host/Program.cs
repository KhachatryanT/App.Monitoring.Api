using System;
using System.Linq;
using System.Text.Json.Serialization;
using App.Monitoring.DataAccess.Dapper.Postgresql;
using App.Monitoring.Infrastructure.Implementation;
using App.Monitoring.Infrastructure.Implementation.Converters;
using App.Monitoring.Infrastructure.Implementation.Hubs;
using App.Monitoring.UseCases;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Information("Загрузка приложения");
try
{
    builder.Configuration.AddEnvironmentVariables("Mobile_");
    builder.Host.UseSerilog(Log.Logger);

    builder.Services.AddControllers()
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            o.JsonSerializerOptions.Converters.Add(new DateTimeOffsetFormatConverter("yyyy-MM-ddTHH:mm:ss.fffZ"));
        });
    builder.Services.AddSignalR();
    builder.Services.AddSwaggerDocument(s =>
    {
        s.PostProcess = doc =>
        {
            doc.Info.Title = "Сервис мониторинга";
        };
    });

    builder.Services.AddNodesUseCases();

    builder.Services.AddCors(o =>
    {
        o.AddDefaultPolicy(p =>
        {
            var allowedDomains = builder.Configuration.GetSection("AllowedDomains").Get<string[]>() ?? Array.Empty<string>();
            p.WithOrigins(allowedDomains)
                .AllowAnyHeader()
                .AllowCredentials();
        });
    });

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(x => x.FullName is not null && x.FullName.StartsWith(nameof(App)))
        .ToArray();
    TypeAdapterConfig.GlobalSettings.Scan(assemblies);

    var postgresqlConnection = builder.Configuration.GetConnectionString("postgresql");
    ArgumentException.ThrowIfNullOrEmpty(postgresqlConnection);

    builder.Services.AddDataAccessDapperPostgresql(postgresqlConnection);
    builder.Services.AddDataAccessDapperPostgresqlMigrator(postgresqlConnection);
    builder.Services.AddNodesToNotificationHubEmitterObserver();

    var app = builder.Build();
    app.MigrateDatabase();

    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwaggerUi3();
    }

    app.UseHttpsRedirection();
    app.UseCors();

    app.MapHub<NotificationHub>("/Notification");
    app.MapControllers();
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Возникло необработанное исключение. Завершение работы.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

/// <summary>
/// Опредедение публичного модификатора доступа.
/// Необходимо для интеграционных тестов.
/// </summary>
public partial class Program
{
}
