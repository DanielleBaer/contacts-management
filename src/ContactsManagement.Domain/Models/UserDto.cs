using System.ComponentModel.DataAnnotations;

namespace ContactsManagement.Domain.Models;

public class UserDto
{
    [Required(ErrorMessage = "User name is mandatory.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is mandatory.")]
    [MinLength(4, ErrorMessage = "The password must contains at least 4 characters.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "O tipo de função é obrigatório.")]
    public RolesTypes Roletype { get; set; }
}
