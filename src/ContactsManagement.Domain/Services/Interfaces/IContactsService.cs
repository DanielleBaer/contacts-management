using ContactsManagement.Domain.Models;

namespace ContactsManagement.Domain.Services.Interfaces;

public interface IContactsService
{
    Task<Guid?> CreateAsync(Contact contact);

    Task<Contact?> UpdateAsync(Contact contact);
}
