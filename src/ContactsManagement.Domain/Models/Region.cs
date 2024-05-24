namespace ContactsManagement.Domain.Models;

public record Region
{
    public int Id { get; init; }

    public Guid NavigationId { get; init; }

    public string? Description { get; init; }

    public string? Ddd { get; init; }
}
