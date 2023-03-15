using FluentMigrator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Monitoring.DataAccess.Dapper.Postgresql.Migrations;

/// <summary>
/// Инициализация таблицы Device_Statistics.
/// </summary>
[Migration(130320231800)]
public sealed class M130320231800_Initial : Migration
{
    /// <inheritdoc />
    public override void Up()
    {
        Create.Table("device_statistics")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("device_type").AsString(20).Nullable()
            .WithColumn("user_name").AsString(100).Nullable()
            .WithColumn("client_version").AsString(20).Nullable()
            .WithColumn("statistic_date").AsDateTime()
            .WithColumn("created").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("modified").AsDateTime().Nullable();

        Execute.Sql(@"CREATE FUNCTION set_modified_date() RETURNS trigger AS $set_modified_date$
    BEGIN
        NEW.modified := NOW();
        RETURN NEW;
    END;
$set_modified_date$ LANGUAGE plpgsql;");

        Execute.Sql(@$"CREATE TRIGGER set_modified_date BEFORE UPDATE ON device_statistics
FOR EACH ROW EXECUTE PROCEDURE set_modified_date();");
    }

    /// <inheritdoc />
    public override void Down()
    {
        Execute.Sql(@"DROP TRIGGER IF EXISTS set_modified_date ON Device_Statistics;");
        Execute.Sql(@"DROP FUNCTION IF EXISTS set_modified_date;");
        Delete.Table("device_statistics");
    }
}
