namespace ContactsManagement.Domain.Models;

public class Users
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    /// <summary>
    /// Defines a type of user
    /// </summary>
    /// <example>SysAdmin</example>
    public UserType? UserType { get; set; }
}

public enum UserType
{
    SysAdmin,
    Operator
}