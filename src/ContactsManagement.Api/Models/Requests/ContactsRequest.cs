using ContactsManagement.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ContactsManagement.Api.Models.Requests;

public class ContactsRequest : IValidatableObject
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Ddd { get; set; }

    [Required]
    public string? PhoneNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (!IsValidEmail(Email))
        {
            yield return new ValidationResult(
                errorMessage: "Invalid email",
                memberNames: new[] { nameof(Email) });
        }

    }

    private bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailRegex);
    }


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
