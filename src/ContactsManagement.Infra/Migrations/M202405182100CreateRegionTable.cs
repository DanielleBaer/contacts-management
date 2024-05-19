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
            .WithColumn("Id")
                .AsInt64()
                .Identity()
                .NotNullable()
                .WithColumnDescription("Region unique identifier")
            .WithColumn("NavigationId")
                .AsGuid()
                .NotNullable()
                .WithColumnDescription("Region identification field of navigation")
            .WithColumn("Description")
                .AsString(50)
                .WithColumnDescription("Region description")
            .WithColumn("Ddd")
                .AsString(2)
                .WithColumnDescription("Region direct distance dialing");

        Create
            .PrimaryKey("IDX_PK_REGION")
            .OnTable(TableName)
            .Column("Id");

        Create
            .Index("IDX_UK_REGION_NAVIGATIONID")
            .OnTable(TableName)
            .OnColumn("NavigationId")
            .Unique();

        Create
            .Index("IDX_SH_REGION_DDD")
            .OnTable(TableName)
            .OnColumn("Ddd");
    }

    public override void Down()
        => Delete.Table(TableName);

}
