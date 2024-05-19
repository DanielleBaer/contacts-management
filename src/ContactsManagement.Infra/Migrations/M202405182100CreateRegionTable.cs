using FluentMigrator;
using System.Diagnostics.CodeAnalysis;

namespace ContactsManagement.Infra.Migrations;

[ExcludeFromCodeCoverage]
[Migration(202405182100)]
public class M202405182100CreateRegionTable : Migration
{
    private const string TableName = "REGION";

    public override void Up()
    {
        Create.Table(TableName)
            .WithColumn("ID")
                .AsInt64()
                .Identity()
                .NotNullable()
                .WithColumnDescription("Region unique identifier")
            .WithColumn("NAVIGATION_ID")
                .AsGuid()
                .NotNullable()
                .WithColumnDescription("Region identification field of navigation")
            .WithColumn("DESCRIPTION")
                .AsString(50)
                .WithColumnDescription("Region description")
            .WithColumn("DDD")
                .AsString(2)
                .WithColumnDescription("Region direct distance dialing");

        Create
            .PrimaryKey("IDX_PK_REGION")
            .OnTable(TableName)
            .Column("ID");

        Create
            .Index("IDX_UK_REGION_NAVIGATION_ID")
            .OnTable(TableName)
            .OnColumn("NAVIGATION_ID")
            .Unique();

        Create
            .Index("IDX_SH_REGION_DDD")
            .OnTable(TableName)
            .OnColumn("DDD");
    }

    public override void Down()
        => Delete.Table(TableName);

}
