using System;
using System.IO;
using System.Text.Json.Serialization;
using App.Monitoring.DataAccess.InMemory;
using App.Monitoring.UseCases;
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
    builder.Services.AddSwaggerGen(o =>
    {
        const string XmlFilename = $"{nameof(App)}.{nameof(App.Monitoring)}.{nameof(App.Monitoring.Api)}.xml";
        o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, XmlFilename));
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

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
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
