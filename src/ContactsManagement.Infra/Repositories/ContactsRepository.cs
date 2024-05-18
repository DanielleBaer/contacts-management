using ContactsManagement.Domain.Models;
using ContactsManagement.Infra.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace ContactsManagement.Infra.Repositories;

public sealed class ContactsRepository : IContactsRepository
{
    private readonly IDbConnection _dbConnection;

    public ContactsRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public IEnumerable<Contacts?> GetAllAsync()
    {
        var sql = ContactsRepositoryStmt.GetAll;

        return _dbConnection.Query<Contacts?>(sql).ToList();
    }
}
