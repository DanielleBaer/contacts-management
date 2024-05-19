namespace ContactsManagement.Domain.Models;

public record Contacts
{
    public Contacts()
    {
        NavigationId = Guid.NewGuid();
    }

    public Guid NavigationId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Ddd { get; set; }

    public string? PhoneNumber { get; set; }

    public Region? Region { get; set; }
}
