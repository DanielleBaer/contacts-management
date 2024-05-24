using ContactsManagement.Domain.Services;
using ContactsManagement.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ContactsManagement.Domain.Extensions;

[ExcludeFromCodeCoverage]
public static class BusinessExtension
{
    public static IServiceCollection AddBusiness(this IServiceCollection services) =>
        services
            .AddScoped<IContactsService, ContactsService>();
}
