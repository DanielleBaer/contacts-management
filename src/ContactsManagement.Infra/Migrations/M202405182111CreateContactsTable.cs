using FluentMigrator;
using System.Diagnostics.CodeAnalysis;

namespace ContactsManagement.Infra.Migrations;

[ExcludeFromCodeCoverage]
[Migration(202405182111)]
public class M202405182111CreateContactsTable : Migration
{
    private const string TableName = "CONTACTS";

    public override void Up()
    {
        Create.Table(TableName)
            .WithColumn("Id")
                .AsInt64()
                .Identity()
                .NotNullable()
                .WithColumnDescription("User unique identifier")
             .WithColumn("NavigationId")
                .AsGuid()
                .NotNullable()
                .WithColumnDescription("User identification field of navigation")
            .WithColumn("Name")
                .AsString(50)
                .WithColumnDescription("User name")
            .WithColumn("Email")
                .AsString(50)
                .WithColumnDescription("User email")
            .WithColumn("Ddd")
                .AsString(2)
                .WithColumnDescription("User direct distance dialing")
            .WithColumn("PhoneNumber")
                .AsString(50)
                .WithColumnDescription("User phone number")
            .WithColumn("RegionId")
                .AsInt64()
                .NotNullable()
                .WithColumnDescription("Foreign key reference from Region"); ;

        Create
            .PrimaryKey("IDX_PK_CONTACTS")
            .OnTable(TableName)
            .Column("Id");

        Create
            .Index("IDX_UK_CONTACTS_NAVIGATIONID")
            .OnTable(TableName)
            .OnColumn("NavigationId")
            .Unique();

        Create
            .Index("IDX_SH_CONTACTS_DDD")
            .OnTable(TableName)
            .OnColumn("Ddd");

        Create
            .ForeignKey()
            .FromTable(TableName)
                .ForeignColumn("RegionId")
            .ToTable("REGION")
                .PrimaryColumn("Id");
    }

    public override void Down()
        => Delete.Table(TableName);
}
