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
        Create.Table("Device_Statistics")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Device_Type").AsString(20).Nullable()
            .WithColumn("User_Name").AsString(100).Nullable()
            .WithColumn("Client_Version").AsString(20).Nullable()
            .WithColumn("Statistic_Date").AsDateTime()
            .WithColumn("Created").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("Modified").AsDateTime().Nullable();

        Execute.Sql(@"CREATE FUNCTION set_modified_date() RETURNS trigger AS $set_modified_date$
    BEGIN
        NEW.Modified := NOW();
        RETURN NEW;
    END;
$set_modified_date$ LANGUAGE plpgsql;");

        Execute.Sql(@$"CREATE TRIGGER set_modified_date BEFORE UPDATE ON Device_Statistics
FOR EACH ROW EXECUTE PROCEDURE set_modified_date();");
    }

    /// <inheritdoc />
    public override void Down()
    {
        Execute.Sql(@"DROP TRIGGER IF EXISTS set_modified_date ON Device_Statistics;");
        Execute.Sql(@"DROP FUNCTION IF EXISTS set_modified_date;");
        Delete.Table("Device_Statistics");
    }
}
