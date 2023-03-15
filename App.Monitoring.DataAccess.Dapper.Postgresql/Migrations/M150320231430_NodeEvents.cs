using FluentMigrator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Monitoring.DataAccess.Dapper.Postgresql.Migrations;

/// <summary>
/// Инициализация таблицы "node_events".
/// Переименование таблицы "device_statistics".
/// </summary>
[Migration(150320231430)]
public sealed class M150320231430_NodeEvents:Migration
{
    /// <inheritdoc />
    public override void Up()
    {
        Create.Table("node_events")
            .WithColumn("id").AsGuid().Identity().PrimaryKey()
            .WithColumn("node_id").AsGuid().NotNullable()
            .WithColumn("name").AsString(50).Nullable()
            .WithColumn("date").AsDateTime()
            .WithColumn("created").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("modified").AsDateTime().Nullable();

        Rename.Table("device_statistics").To("nodes");
        Alter.Table("nodes").AlterColumn("statistic_date").AsDateTime().Nullable();

        Create.ForeignKey()
            .FromTable("node_events").ForeignColumn("node_id")
            .ToTable("nodes").PrimaryColumn("id");

        Execute.Sql(@$"CREATE TRIGGER set_modified_date BEFORE UPDATE ON node_events FOR EACH ROW EXECUTE PROCEDURE set_modified_date();");
    }

    /// <inheritdoc />
    public override void Down()
    {
        Execute.Sql(@"DROP TRIGGER IF EXISTS set_modified_date ON node_events;");
        Delete.Table("node_events");
        Rename.Table("nodes").To("device_statistics");
        Alter.Table("device_statistics").AlterColumn("statistic_date").AsDateTime().NotNullable();
    }
}
