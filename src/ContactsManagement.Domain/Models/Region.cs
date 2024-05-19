namespace ContactsManagement.Domain.Models;

public record Region
{
    public Guid NavigationId { get; set; }

    public string? Description { get; set; }

    public string? Ddd { get; set; }
}
