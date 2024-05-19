using ContactsManagement.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ContactsManagement.Api.Models.Requests;

public class ContactsRequest
{
    [Required]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^\d{2}$", ErrorMessage = "The DDD field must have a maximum of 2 numerical characters.")]
    public string? Ddd { get; set; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    internal Contact ToDomainModel(Guid? id = null)
    {
        return new()
        {
            NavigationId = id ?? Guid.NewGuid(),
            Name = Name,
            Email = Email,
            Ddd = Ddd,
            PhoneNumber = PhoneNumber,
        };
    }
}
