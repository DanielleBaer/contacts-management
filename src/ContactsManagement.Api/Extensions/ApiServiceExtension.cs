using ContactsManagement.Ioc;
using System.Diagnostics.CodeAnalysis;

namespace ContactsManagement.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ApiServiceExtension
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services) =>
        services
            .ConfigSwagger()
            .IocConfig();
}
