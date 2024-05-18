using ContactsManagement.Domain.Models;

namespace ContactsManagement.Infra.Repositories.Interfaces;

public interface IContactsRepository
{
    IEnumerable<Contacts?> GetAllAsync();
}
