using ContactsManagement.Domain.Models;

namespace ContactsManagement.Domain.Repositories;

public interface IRegionRepository
{
    Task<Region?> GetByDddAsync(string ddd);
}
