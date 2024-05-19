using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using ContactsManagement.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContactsManagement.Domain.Services;

public class ContactsService : IContactsService
{
    private readonly IContactsRepository _contactsRepository;
    private readonly IRegionRepository _regionRepository;
    private readonly ILogger<ContactsService> _logger;

    public ContactsService(
        IContactsRepository contactsRepository,
        IRegionRepository regionRepository,
        ILogger<ContactsService> logger)
    {
        _contactsRepository = contactsRepository;
        _regionRepository = regionRepository;
        _logger = logger;

    }

    public async Task<Guid?> CreateAsync(Contact contact)
    {
        var region = await _regionRepository.GetByDddAsync(contact.Ddd!);

        if (region is null)
        {
            _logger.LogWarning("Region for ddd '{Ddd}' was not found", contact.Ddd);
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
            _logger.LogWarning("Region for ddd '{Ddd}' was not found", contact.Ddd);
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
