using FluentMigrator;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactsManagement.Infra.Migrations;

[ExcludeFromCodeCoverage]
[Migration(202405202232)]
public class M202405202232SeedRegion : Migration
{
    private const string sqlInsert =
        @"INSERT INTO ""REGION""(
                ""NavigationId"",
                ""Description"",
                ""Ddd""
             ) VALUES
        ";
    private const string sqlParams = 
        @"(
            '{0}',
            '{1}',
            '{2}'
            ),
        ";

    public override void Up()
    {
        var insertData = new StringBuilder(sqlInsert);

        insertData.AppendFormat(
            sqlParams,
            Guid.Parse("5D6F9192-D22B-4F0F-B083-A2F5489AD8F5"),
            "Santa Catarina",
            "47");

        insertData.AppendFormat(
            sqlParams,
            Guid.Parse("B51C6853-5BD8-4618-82C9-38F24E57FA7F"),
            "Santa Catarina",
            "48");

        insertData.AppendFormat(
            sqlParams,
            Guid.Parse("7622B5C5-12D8-40C0-BFEE-25A2EADE0FE9"),
            "São Paulo",
            "11");

        insertData.AppendFormat(
            sqlParams,
            Guid.Parse("E45E6E74-29A1-4831-9662-8607980ED4D2"),
            "São Paulo",
            "15");

        insertData.AppendFormat(
            sqlParams,
            Guid.Parse("143F626D-7954-4C2A-87C5-B004BCA79DBD"),
            "Paraná",
            "44");

        insertData.AppendFormat(
            Regex.Replace(sqlParams.TrimEnd(), ".$", ";"),
            Guid.Parse("2F5F93F4-D09C-41DD-8067-D186A66E36D6"),
            "Rio de Janeiro",
            "21");

        Execute.Sql(insertData.ToString(), "Seeding data to Region table");
    }

    public override void Down()
    {
        Delete.FromTable("REGION");
    }
}
