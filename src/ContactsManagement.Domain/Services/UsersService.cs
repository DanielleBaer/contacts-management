using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using ContactsManagement.Domain.Services.Interfaces;

namespace ContactsManagement.Domain.Services;

public class UsersService : IUsersService
{
    public readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<User?> GetUser(string userName, string password)
        => await _usersRepository.GetUser(userName, password);

}
