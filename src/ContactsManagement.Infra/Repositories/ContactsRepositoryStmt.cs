namespace ContactsManagement.Infra.Repositories;

internal static class ContactsRepositoryStmt
{
    public const string SelectByNavigationId = SelectResultSet + "\nWHERE c.\"NavigationId\" = @NavigationId;";

    public const string SelectById = SelectResultSet + "\nWHERE c.\"Id\" = @Id;";

    public const string SelectByDdd = SelectResultSet + "\nWHERE c.\"Ddd\" = @Ddd;";

    public const string SelectResultSet =
        @"
            SELECT DISTINCT
                c.""NavigationId"",
                c.""Name"",
                c.""Email"",
                c.""Ddd"",
                c.""PhoneNumber"",
                r.""Description"" as RegionDescription
              FROM ""CONTACTS"" c
             INNER JOIN ""REGION"" r 
                  ON r.""Id"" = c.""RegionId""
        ";

    public const string Insert =
        @"
            INSERT INTO ""CONTACTS""(
                ""NavigationId"",
                ""Name"",
                ""Email"",
                ""Ddd"",
                ""PhoneNumber"",
                ""RegionId""
            )
            VALUES (
                @NavigationId,
                @Name,
                @Email,
                @Ddd,
                @PhoneNumber,
                @RegionId
            )
            RETURNING ""Id"";
        ";

    public const string Update =
        @"
            UPDATE ""CONTACTS""
            SET
                ""Name"" = @Name,
                ""Email"" = @Email,
                ""Ddd"" = @Ddd,
                ""PhoneNumber"" = @PhoneNumber,
                ""RegionId"" = @RegionId
            WHERE
                ""NavigationId"" = @NavigationId
            RETURNING ""Id"";
        ";

    public const string Delete =
        @"
            DELETE
              FROM ""CONTACTS""
             WHERE ""NavigationId"" = @NavigationId;
        ";
}
