using App.Monitoring.DataAccess.Dapper.Postgresql.Entities;
using FluentMigrator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Monitoring.DataAccess.Dapper.Postgresql.Migrations;

/// <summary>
/// Инициализация таблиц <see cref="DeviceStatisticEntity"/>.
/// </summary>
[Migration(130320231800)]
public sealed class M130320231800_Initial : Migration
{
    /// <inheritdoc />
    public override void Up()
    {
        Create.Table(nameof(DeviceStatisticEntity))
            .WithColumn(nameof(DeviceStatisticEntity.Id)).AsGuid().PrimaryKey()
            .WithColumn(nameof(DeviceStatisticEntity.DeviceType)).AsString(20).Nullable()
            .WithColumn(nameof(DeviceStatisticEntity.UserName)).AsString(100).Nullable()
            .WithColumn(nameof(DeviceStatisticEntity.ClientVersion)).AsString(20).Nullable()
            .WithColumn(nameof(DeviceStatisticEntity.StatisticDate)).AsDateTime()
            .WithColumn("Created").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("Modified").AsDateTime().Nullable();

        Execute.Sql(@"CREATE FUNCTION set_modified_date() RETURNS trigger AS $set_modified_date$
    BEGIN
        NEW.Modified := NOW();
        RETURN NEW;
    END;
$set_modified_date$ LANGUAGE plpgsql;");

        Execute.Sql(@$"CREATE TRIGGER set_modified_date BEFORE UPDATE ON {nameof(DeviceStatisticEntity)}
    FOR EACH ROW EXECUTE PROCEDURE set_modified_date();");
    }

    /// <inheritdoc />
    public override void Down()
    {
        Execute.Sql(@$"DROP TRIGGER IF EXISTS set_modified_date ON {nameof(DeviceStatisticEntity)};");
        Execute.Sql(@$"DROP FUNCTION IF EXISTS set_modified_date;");
        Delete.Table(nameof(DeviceStatisticEntity));
    }
}
