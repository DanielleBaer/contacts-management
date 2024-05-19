namespace ContactsManagement.Domain.Models;

public record User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public RolesTypes RoleType { get; set; }
}

public enum RolesTypes
{
    Admin,
    Employee
}

public static class Roles
{
    public const string Admin = "Administrator";
    public const string NotAdmin = "Employee";
}