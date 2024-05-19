namespace ContactsManagement.Domain.Models.Responses;

public readonly record struct ContactsResponse
{
    public Guid NavigationId { get; init; }

    public string? Name { get; init; }

    public string? Email { get; init; }

    public string? PhoneNumber { get; init; }

    public string? RegionDescription { get; init; }

    public static ContactsResponse From(Contact contacts) => new()
    {
        NavigationId = contacts.NavigationId,
        Name = contacts.Name,
        Email = contacts.Email,
        PhoneNumber = contacts.Ddd + contacts.PhoneNumber,
        RegionDescription = contacts.RegionDescription,
    };
}
