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
            .WithColumn("ID")
                .AsInt64()
                .Identity()
                .NotNullable()
                .WithColumnDescription("User unique identifier")
             .WithColumn("NAVIGATION_ID")
                .AsGuid()
                .NotNullable()
                .WithColumnDescription("User identification field of navigation")
            .WithColumn("NAME")
                .AsString(50)
                .WithColumnDescription("User name")
            .WithColumn("EMAIL")
                .AsString(50)
                .WithColumnDescription("User email")
            .WithColumn("DDD")
                .AsString(2)
                .WithColumnDescription("User direct distance dialing")
            .WithColumn("PHONE_NUMBER")
                .AsString(50)
                .WithColumnDescription("User phone number")
            .WithColumn("REGION_ID")
                .AsInt64()
                .NotNullable()
                .WithColumnDescription("Foreign key reference from Region"); ;

        Create
            .PrimaryKey("IDX_PK_CONTACTS")
            .OnTable(TableName)
            .Column("ID");

        Create
            .Index("IDX_UK_CONTACTS_NAVIGATION_ID")
            .OnTable(TableName)
            .OnColumn("NAVIGATION_ID")
            .Unique();

        Create
            .Index("IDX_SH_CONTACTS_DDD")
            .OnTable(TableName)
            .OnColumn("DDD");

        Create
            .ForeignKey()
            .FromTable(TableName)
                .ForeignColumn("REGION_ID")
            .ToTable("REGION")
                .PrimaryColumn("ID");
    }

    public override void Down()
        => Delete.Table(TableName);
}
