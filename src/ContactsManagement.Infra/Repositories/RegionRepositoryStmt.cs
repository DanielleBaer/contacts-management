namespace ContactsManagement.Infra.Repositories;

public static class RegionRepositoryStmt
{
    internal const string SelectByDdd = SelectResultSet + "\nWHERE r.\"Ddd\" = @Ddd;";

    internal const string SelectResultSet = 
        @"
            SELECT
                r.""Id"",
                r.""NavigationId"",
                r.""Description"",
                r.""Ddd""
              FROM ""REGION"" r";
}
