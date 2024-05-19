namespace ContactsManagement.Infra.Repositories;

public static class UsersRepositoryStmt
{
    internal const string SelectByLogin = 
        @"
            SELECT *
              FROM ""USERS""
             WHERE ""Username"" = @Username
               AND ""Password"" = @Password
        ";
}
