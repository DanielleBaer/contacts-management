using ContactsManagement.Api.Controllers;
using ContactsManagement.Api.Models.Requests;
using ContactsManagement.Api.Tests.Fixture;
using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Models.Responses;
using ContactsManagement.Domain.Repositories;
using ContactsManagement.Domain.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ContactsManagement.Api.Tests.Controllers;

public class ContactsControllerTest : IDisposable
{
    private readonly Mock<IContactsRepository> _contactsRepositoryMock;
    private readonly Mock<IRegionRepository> _regionRepository;
    private readonly Mock<IContactsService> _contactsServiceMock;

    public ContactsControllerTest()
    {
        _contactsRepositoryMock = new(MockBehavior.Strict);
        _regionRepository = new(MockBehavior.Strict);
        _contactsServiceMock = new(MockBehavior.Strict);
    }

    public void Dispose()
        => Mock.VerifyAll(_contactsRepositoryMock);

    [Fact]
    public async Task Should_ReturnListOfContacts_When_AnyRecordWasFound()
    {
        // Arrange
        var contacts = ContactFixture.Generate(2);
        var expectedResult = new OkObjectResult(contacts.Select(ContactsResponse.From));
        _contactsRepositoryMock
            .Setup(m => m.GetAllAsync())
            .ReturnsAsync(contacts);

        var controller = GetController();

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnEmptyList_When_NoRecordsWereFound()
    {
        // Arrange
        var contacts = new List<Contact>();
        var expectedResult = new OkObjectResult(Enumerable.Empty<ContactsResponse>());
        _contactsRepositoryMock
            .Setup(m => m.GetAllAsync())
            .ReturnsAsync(contacts);

        var controller = GetController();

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnContact_When_TheRecordWasFoundById()
    {
        // Arrange
        var contact = ContactFixture.Generate();
        var expectedResult = new OkObjectResult(ContactsResponse.From(contact));
        _contactsRepositoryMock
            .Setup(m => m.GetByNavigationIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contact);

        var controller = GetController();

        // Act
        var result = await controller.GetByIdAsync(contact.NavigationId);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnNotFound_When_NoRecordWasFoundById()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var expectedResult = new NotFoundResult();
        _contactsRepositoryMock
            .Setup(m => m.GetByNavigationIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Contact);

        var controller = GetController();

        // Act
        var result = await controller.GetByIdAsync(contactId);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnContact_When_TheRecordWasFoundByDdd()
    {
        // Arrange
        var contact = ContactFixture.Generate();
        var expectedResult = new OkObjectResult(ContactsResponse.From(contact));
        _contactsRepositoryMock
            .Setup(m => m.GetByDddAsync(It.IsAny<string>()))
            .ReturnsAsync(contact);

        var controller = GetController();

        // Act
        var result = await controller.GetByDddAsync(contact.Ddd!);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnNotFound_When_NoRecordWasFoundByDdd()
    {
        // Arrange
        var contactDdd = "47";
        var expectedResult = new NotFoundResult();
        _contactsRepositoryMock
            .Setup(m => m.GetByDddAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Contact);

        var controller = GetController();

        // Act
        var result = await controller.GetByDddAsync(contactDdd);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnOk_When_TheRecordToDeleteWasFound()
    {
        // Arrange
        var contact = ContactFixture.Generate();
        var expectedResult = new OkResult();
        _contactsRepositoryMock
            .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var controller = GetController();

        // Act
        var result = await controller.DeleteAsync(contact.NavigationId);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnNotFoud_When_TheRecordToDeleteWasNotFound()
    {
        // Arrange
        var contact = ContactFixture.Generate();
        var expectedResult = new NotFoundResult();
        _contactsRepositoryMock
            .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var controller = GetController();

        // Act
        var result = await controller.DeleteAsync(contact.NavigationId);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    private ContactsController GetController()
        => new(_contactsRepositoryMock.Object, _contactsServiceMock.Object);
}
