namespace ContactsManagement.Domain.Models;

public record Contact
{
    public Guid NavigationId { get; init; }

    public string? Name { get; init; }

    public string? Email { get; init; }

    public string? Ddd { get; init; }

    public string? PhoneNumber { get; init; }

    public int? RegionId { get; init; }

    public string? RegionDescription { get; init; }
}
