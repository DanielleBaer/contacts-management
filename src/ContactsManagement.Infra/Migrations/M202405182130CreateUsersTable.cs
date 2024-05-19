using FluentMigrator;
using System.Diagnostics.CodeAnalysis;

namespace ContactsManagement.Infra.Migrations;

[ExcludeFromCodeCoverage]
[Migration(202405182130)]
public class M202405182130CreateUsersTable : Migration
{
    private const string TableName = "USERS";

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
            .WithColumn("Username")
                .AsString(50)
                .WithColumnDescription("User name")
            .WithColumn("Password")
                .AsString(100)
                .WithColumnDescription("User password")
            .WithColumn("RoleType")
                .AsString(20)
                .NotNullable()
                .WithColumnDescription("User role type");

        Create
            .PrimaryKey("IDX_PK_USERS")
            .OnTable(TableName)
            .Column("Id");

    }

    public override void Down()
        => Delete.Table(TableName);
}
