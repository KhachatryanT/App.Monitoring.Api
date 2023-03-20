using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace App.Monitoring.Host.IntegrationTests;

/// <summary>
/// Фабрика запуска приложения.
/// </summary>
public class AppWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <inheritdoc/>
    public override ValueTask DisposeAsync()
    {
        TruncateDatabase(Services.GetRequiredService<NpgsqlConnection>());
        return base.DisposeAsync();
    }

    /// <inheritdoc/>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.IntegrationTests.json")
            .Build();

        EnsureTruncatedDatabase(configuration);

        builder.UseConfiguration(configuration)
            .UseEnvironment("IntegrationTests");
    }

    private static void EnsureTruncatedDatabase(IConfiguration configuration)
    {
        using var cnn = new NpgsqlConnection(configuration.GetConnectionString("postgres"));
        cnn.Open();
        TruncateDatabase(cnn);
        cnn.Close();
    }

    private static void TruncateDatabase(NpgsqlConnection cnn) => cnn.Execute(@"
DO $$
DECLARE
  tables CURSOR FOR
    SELECT tablename
    FROM pg_catalog.pg_tables
    WHERE schemaname = 'public' and tablename != 'versioninfo';
BEGIN
  FOR t IN tables LOOP
  EXECUTE 'TRUNCATE TABLE ' || quote_ident(t.tablename) || ' CASCADE;';
  END LOOP;
END;
$$");
}
