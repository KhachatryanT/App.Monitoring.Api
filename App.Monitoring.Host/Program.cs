using System;
using System.Linq;
using System.Text.Json.Serialization;
using App.Monitoring.DataAccess.InMemory;
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
    builder.Host.UseSerilog(Log.Logger);

    builder.Services.AddControllers()
        .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    builder.Services.AddSwaggerDocument(s =>
    {
        s.PostProcess = doc =>
        {
            doc.Info.Title = "Сервис мониторинга";
        };
    });

    builder.Services.AddDeviceStatisticsUseCases();
    builder.Services.AddDataAccessInMemory();

    builder.Services.AddCors(o =>
    {
        o.AddDefaultPolicy(p =>
        {
            var allowedDomains = builder.Configuration.GetSection("AllowedDomains").Get<string[]>() ?? Array.Empty<string>();
            p.WithOrigins(allowedDomains)
                .AllowAnyHeader();
        });
    });

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(x => x.FullName is not null && x.FullName.StartsWith(nameof(App)))
        .ToArray();
    TypeAdapterConfig.GlobalSettings.Scan(assemblies);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwaggerUi3();
    }

    app.UseHttpsRedirection();
    app.UseCors();

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
