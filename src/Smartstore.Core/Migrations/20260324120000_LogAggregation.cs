using FluentMigrator;
using Smartstore.Core.Data.Migrations;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Migrations;

[MigrationVersion("2026-03-24 12:00:00", "Core: Log aggregation")]
internal class LogAggregation : Migration, ILocaleResourcesProvider
{
    const string TableName = "Log";

    public override void Up()
    {
        if (!Schema.Table(TableName).Column("OccurrenceCount").Exists())
        {
            Create.Column("OccurrenceCount").OnTable(TableName)
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(1);
        }

        if (!Schema.Table(TableName).Column("Occurrences").Exists())
        {
            Create.Column("Occurrences").OnTable(TableName)
                .AsMaxString()
                .Nullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(TableName).Column("OccurrenceCount").Exists())
        {
            Delete.Column("OccurrenceCount").FromTable(TableName);
        }

        if (Schema.Table(TableName).Column("Occurrences").Exists())
        {
            Delete.Column("Occurrences").FromTable(TableName);
        }
    }

    public DataSeederStage Stage => DataSeederStage.Early;
    public bool AbortOnFailure => false;

    public void MigrateLocaleResources(LocaleResourcesBuilder builder)
    {
        builder.AddOrUpdate("Admin.System.Log.Fields.Occurrences",
            "Occurrences",
            "Vorkommen",
            "Number of times this event was repeated within the 10-minute aggregation window.",
            "Gibt an, wie oft dieses Ereignis innerhalb des 10-minütigen Aggregationsfensters aufgetreten ist.");
    }
}