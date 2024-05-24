using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using ContactsManagement.Domain.Services.Interfaces;

namespace ContactsManagement.Domain.Services;

public class ContactsService : IContactsService
{
    private readonly IContactsRepository _contactsRepository;
    private readonly IRegionRepository _regionRepository;

    public ContactsService(
        IContactsRepository contactsRepository,
        IRegionRepository regionRepository)
    {
        _contactsRepository = contactsRepository;
        _regionRepository = regionRepository;

    }

    public async Task<Guid?> CreateAsync(Contact contact)
    {
        var region = await _regionRepository.GetByDddAsync(contact.Ddd!);

        if (region is null)
        {
            return Guid.Empty;
        }

        var newContact = contact with
        {
            RegionId = region.Id,
            RegionDescription = region.Description
        };

        var contactCreated = await _contactsRepository.CreateAsync(newContact);

        return contactCreated;
    }

    public async Task<Contact?> UpdateAsync(Contact contact)
    {
        var region = await _regionRepository.GetByDddAsync(contact.Ddd!);

        if (region is null)
        {
            return null;
        }

        var newContact = contact with
        {
            RegionId = region.Id,
            RegionDescription = region.Description
        };

        var contactUpdated = await _contactsRepository.UpdateAsync(newContact);

        return contactUpdated;
    }
}
