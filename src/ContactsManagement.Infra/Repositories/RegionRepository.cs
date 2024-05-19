using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using Dapper;
using System.Data;

namespace ContactsManagement.Infra.Repositories;

public sealed class RegionRepository : IRegionRepository
{
    private readonly IDbConnection _dbConnection;

    public RegionRepository(IDbConnection dbConnection)
        => _dbConnection = dbConnection;


    public async Task<Region?> GetByDddAsync(string ddd)
    {
        return await _dbConnection
            .QuerySingleOrDefaultAsync<Region>(
                sql: RegionRepositoryStmt.SelectByDdd,
                param: new { ddd });
    }
}
