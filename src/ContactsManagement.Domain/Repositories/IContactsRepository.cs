using ContactsManagement.Domain.Models;

namespace ContactsManagement.Domain.Repositories;

public interface IContactsRepository
{
    Task<IEnumerable<Contact?>> GetAllAsync();

    Task<Contact?> GetByNavigationIdAsync(Guid id);

    Task<Contact?> GetByIdAsync(int id);

    Task<Contact?> GetByDddAsync(string ddd);

    Task<Guid?> CreateAsync(Contact contacts);

    Task<Contact?> UpdateAsync(Contact contact);

    Task<bool> DeleteAsync(Guid contactId);
}