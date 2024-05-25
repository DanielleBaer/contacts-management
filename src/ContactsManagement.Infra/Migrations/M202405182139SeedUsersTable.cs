using FluentMigrator;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactsManagement.Infra.Migrations;

[ExcludeFromCodeCoverage]
[Migration(202405182139)]
public class M202405182139SeedUsersTable : Migration
{
    private const string sqlInsert =
        @"INSERT INTO ""USERS""(
                ""NavigationId"",
                ""Username"",
                ""Password"",
                ""RoleType""
             ) VALUES
        ";
    private const string sqlParams =
        @"(
            '{0}',
            '{1}',
            '{2}',
            '{3}'
            ),
        ";

    public override void Up()
    {
        var insertData = new StringBuilder(sqlInsert);

        insertData.AppendFormat(
            sqlParams,
            Guid.Parse("A8EAA045-2619-472B-9268-E809B85F9D96"),
            "Admin",
            "1234",
            "admin");

        insertData.AppendFormat(
            Regex.Replace(sqlParams.TrimEnd(), ".$", ";"),
            Guid.Parse("5B96416D-28A9-49CE-86AA-177C9C22FD47"),
            "NotAdmin",
            "4321",
            "employee");

        Execute.Sql(insertData.ToString(), "Seeding data to Users table");
    }

    public override void Down()
    {
        Delete.FromTable("USERS");
    }
}
