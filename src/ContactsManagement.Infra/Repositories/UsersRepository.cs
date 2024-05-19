using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using Dapper;
using System.Data;

namespace ContactsManagement.Infra.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IDbConnection _dbContext;

    public UsersRepository(IDbConnection dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUser(string userName, string password)
        => await _dbContext
            .QueryFirstOrDefaultAsync<User>(
                sql: UsersRepositoryStmt.SelectByLogin,
                param: new { userName, password });
}
