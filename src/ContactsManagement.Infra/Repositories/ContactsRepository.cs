using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using Dapper;
using System.Data;
using System.Data.Common;

namespace ContactsManagement.Infra.Repositories;

public sealed class ContactsRepository : IContactsRepository
{
    private readonly IDbConnection _dbConnection;

    public ContactsRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Guid?> CreateAsync(Contact contact)
    {
        try
        {
            var contactId = await _dbConnection.QuerySingleAsync<int>(
            sql: ContactsRepositoryStmt.Insert,
            param: new
            {
                contact.NavigationId,
                contact.Name,
                contact.Email,
                contact.Ddd,
                contact.PhoneNumber,
                contact.RegionId
            });

            var contactCreated = await GetByIdAsync(contactId);

            return contactCreated!.NavigationId;
        }
        catch (DbException)
        {
            return Guid.Empty;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Contact?> UpdateAsync(Contact contact)
    {
        try
        {
            var contactId = await _dbConnection.QuerySingleAsync<int>(
            sql: ContactsRepositoryStmt.Update,
            param: new
            {
                contact.NavigationId,
                contact.Name,
                contact.Email,
                contact.Ddd,
                contact.PhoneNumber,
                contact.RegionId
            });

            var contactUpdated = await GetByIdAsync(contactId);

            return contactUpdated;
        }
        catch (DbException)
        {
            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid navigationId)
    {
        try
        {
            var affectedRows = await _dbConnection.ExecuteAsync(
                sql: ContactsRepositoryStmt.Delete,
                param: new { navigationId });

            if(affectedRows <= 0)
            {
                return false;
            }

            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Contact?> GetByIdAsync(int id)
        => await _dbConnection
            .QuerySingleOrDefaultAsync<Contact>(
                sql: ContactsRepositoryStmt.SelectById,
                param: new { id });

    public async Task<Contact?> GetByNavigationIdAsync(Guid navigationId)
        => await _dbConnection
            .QuerySingleOrDefaultAsync<Contact>(
                sql: ContactsRepositoryStmt.SelectByNavigationId,
                param: new { navigationId });

    public async Task<Contact?> GetByDddAsync(string ddd)
        => await _dbConnection
            .QuerySingleOrDefaultAsync<Contact>(
                sql: ContactsRepositoryStmt.SelectByDdd,
                param: new { ddd });

    public async Task<IEnumerable<Contact?>> GetAllAsync()
        => await _dbConnection
        .QueryAsync<Contact?>(sql: ContactsRepositoryStmt.SelectResultSet + @" ORDER BY ""Name"" ASC");
}
