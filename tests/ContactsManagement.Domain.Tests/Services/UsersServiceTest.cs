using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using ContactsManagement.Domain.Services;
using Moq;

namespace ContactsManagement.Domain.Tests.Services;

public class UsersServiceTest
{
    private readonly Mock<IUsersRepository> _usersRepositoryMock;

    public UsersServiceTest()
    {
        _usersRepositoryMock = new(MockBehavior.Strict);
    }

    [Fact]
    public async Task Should_GetUser_When_UserWasFound()
    {
        // Arrange
        string username = "testuser";
        string password = "testpassword";
        var service = BuildService();
        var expectedUser = new User { Id = 1, Username = username, Password = password };
        _usersRepositoryMock.Setup(r => r.GetUser(username, password)).ReturnsAsync(expectedUser);

        // Act
        var result = await service.GetUser(username, password);

        // Assert
        Assert.Equal(expectedUser, result);
    }

    private UsersService BuildService()
        => new(_usersRepositoryMock.Object);
}
