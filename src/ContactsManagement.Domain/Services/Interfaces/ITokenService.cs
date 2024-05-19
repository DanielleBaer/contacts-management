using ContactsManagement.Domain.Models;

namespace ContactsManagement.Domain.Services.Interfaces;

public interface ITokenService
{
    public Task<string> GetToken(UserDto user);
}
