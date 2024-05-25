using ContactsManagement.Domain.Models;

namespace ContactsManagement.Domain.Services.Interfaces;

public interface IUsersService
{
    Task<User?> GetUser(string userName, string password);
}
