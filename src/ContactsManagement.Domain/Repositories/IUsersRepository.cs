using ContactsManagement.Domain.Models;

namespace ContactsManagement.Domain.Repositories;

public interface IUsersRepository
{
    public Task<User?> GetUser(string userName, string password);
}
