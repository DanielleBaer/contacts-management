using ContactsManagement.Infra.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using ContactsManagement.Domain.Extensions;

namespace ContactsManagement.Ioc;

[ExcludeFromCodeCoverage]
public static class IoC
{
    public static IServiceCollection IocConfig(this IServiceCollection services) =>
        services
            .AddDapperInfra()
            .AddBusiness();
}
