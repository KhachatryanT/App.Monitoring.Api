using System;
using System.IO;
using System.Text.Json.Serialization;
using App.Monitoring.DataAccess.InMemory;
using App.Monitoring.Infrastructure.Implementation.Application;
using App.Monitoring.Infrastructure.Interfaces.DataAccess;
using App.Monitoring.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog(Log.Logger);

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddSwaggerGen(o =>
{
    const string XmlFilename = $"{nameof(App)}.{nameof(App.Monitoring)}.{nameof(App.Monitoring.Controllers)}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, XmlFilename));
});

builder.Services.AddUseCases();
builder.Services.AddDbContext<IDbContext, AppDbContext>(o => o.UseInMemoryDatabase("app"));

var app = builder.Build();
await app.EnsureMigrationAsync<AppDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseExceptionHandler(b => b.Run(ExceptionHandling.ExceptionLog));
app.UseHttpsRedirection();

//app.UseActivityLogger();
app.MapControllers();
app.Run();
