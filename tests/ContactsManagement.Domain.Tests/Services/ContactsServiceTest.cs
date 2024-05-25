using Bogus;
using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Repositories;
using ContactsManagement.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace ContactsManagement.Domain.Tests.Services;

public class ContactsServiceTest
{
    private static readonly Faker _faker = new();
    private readonly Mock<IContactsRepository> _contactsRepositoryMock;
    private readonly Mock<IRegionRepository> _regionRepositoryMock;
    private readonly Mock<ILogger<ContactsService>> _loggerMock;

    public ContactsServiceTest()
    {
        _contactsRepositoryMock = new(MockBehavior.Strict);
        _regionRepositoryMock = new(MockBehavior.Strict);
        _loggerMock = new(MockBehavior.Default);
    }

    [Fact]
    public async Task Should_ReturnContactId_When_TheRecordWasSavedSuccessfully()
    {
        // Arrange
        var contact = new Contact()
        {
            NavigationId = _faker.Random.Guid(),
            Name = _faker.Random.Word(),
            Email = _faker.Internet.Email(),
            Ddd = _faker.Random.Number(2).ToString(),
            PhoneNumber = _faker.Phone.PhoneNumber(),
            RegionId = _faker.Random.Number(),
            RegionDescription = _faker.Random.Word(),
        };

        var region = new Region()
        {
            NavigationId = contact.NavigationId,
            Id = contact.RegionId.Value,
            Description = contact.RegionDescription,
            Ddd = contact.Ddd,
        };

        _regionRepositoryMock
            .Setup(m => m.GetByDddAsync(It.IsAny<string>()))
            .ReturnsAsync(region);

        _contactsRepositoryMock
            .Setup(m => m.CreateAsync(It.IsAny<Contact>()))
            .ReturnsAsync(contact.NavigationId);

        var service = BuildService();

        // Act
        var result = await service.CreateAsync(contact);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(contact.NavigationId);
    }

    [Fact]
    public async Task Should_ReturnGuidEmpty_When_RegionIsNull()
    {
        // Arrange
        var contact = new Contact();

        _regionRepositoryMock
            .Setup(m => m.GetByDddAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Region);

        var service = BuildService();

        // Act
        var result = await service.CreateAsync(contact);

        // Assert
        result.Should().Be(Guid.Empty);
        _loggerMock.Verify();
    }

    [Fact]
    public async Task Should_ReturnContactId_When_TheRecordWasUpdatedSuccessfully()
    {
        // Arrange
        var contact = new Contact()
        {
            NavigationId = _faker.Random.Guid(),
            Name = _faker.Random.Word(),
            Email = _faker.Internet.Email(),
            Ddd = _faker.Random.Number(2).ToString(),
            PhoneNumber = _faker.Phone.PhoneNumber(),
            RegionId = _faker.Random.Number(),
            RegionDescription = _faker.Random.Word(),
        };

        var region = new Region()
        {
            NavigationId = contact.NavigationId,
            Id = contact.RegionId.Value,
            Description = contact.RegionDescription,
            Ddd = contact.Ddd,
        };

        _regionRepositoryMock
            .Setup(m => m.GetByDddAsync(It.IsAny<string>()))
            .ReturnsAsync(region);

        _contactsRepositoryMock
            .Setup(m => m.UpdateAsync(It.IsAny<Contact>()))
            .ReturnsAsync(contact);

        var service = BuildService();

        // Act
        var result = await service.UpdateAsync(contact);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(contact);
    }

    [Fact]
    public async Task Should_ReturnNull_When_RegionIsNull()
    {
        // Arrange
        var contact = new Contact();

        _regionRepositoryMock
            .Setup(m => m.GetByDddAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Region);

        var service = BuildService();

        // Act
        var result = await service.UpdateAsync(contact);

        // Assert
        result.Should().BeNull();
        _loggerMock.Verify();
    }

    private ContactsService BuildService() => new(
        _contactsRepositoryMock.Object,
        _regionRepositoryMock.Object,
        _loggerMock.Object);
}


