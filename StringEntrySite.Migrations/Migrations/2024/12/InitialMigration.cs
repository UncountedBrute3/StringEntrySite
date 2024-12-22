using FluentMigrator;

namespace StringEntrySite.Migrations.Migrations._2024._12;

[TimestampedMigration(2024, 12, 22, 6, 31)]
public class InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("StringEntries")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn("Value").AsString(500).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("StringEntries");
    }
}