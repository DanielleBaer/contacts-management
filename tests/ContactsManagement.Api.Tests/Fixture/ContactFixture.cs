using Bogus;
using ContactsManagement.Api.Models.Requests;
using ContactsManagement.Domain.Models;

namespace ContactsManagement.Api.Tests.Fixture;

internal static class ContactFixture
{
    private static readonly Faker _faker = new();

    internal static Contact Generate() => new()
    {
        NavigationId = _faker.Random.Guid(),
        Name = _faker.Random.Word(),
        Email = _faker.Internet.Email(),
        Ddd = _faker.Random.Number(2).ToString(),
        PhoneNumber = _faker.Phone.PhoneNumber(),
        RegionId = _faker.Random.Number(),
        RegionDescription = _faker.Random.Word(),
    };

    internal static IEnumerable<Contact> Generate(int count)
        => _faker.Make(count, () => Generate());

    internal static ContactsRequest GenerateRequest() => new()
    {
        Name = _faker.Random.Word(),
        Email = _faker.Internet.Email(),
        Ddd = _faker.Random.Number(2).ToString(),
        PhoneNumber = _faker.Phone.PhoneNumber(),
    };
}
