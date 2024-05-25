using ContactsManagement.Domain.Repositories;
using ContactsManagement.Infra.Repositories;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ContactsManagement.Infra.Extensions;

[ExcludeFromCodeCoverage]
public static class InfraServiceExtension
{
    private const string ConnectionString = "Default";

    public static IServiceCollection AddDapperInfra(this IServiceCollection services) =>
        services
            .AddDbConnection()
            .AddRepositories()
            .ConfigureFluentMigrator();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IContactsRepository, ContactsRepository>()
            .AddScoped<IRegionRepository, RegionRepository>()
            .AddScoped<IUsersRepository, UsersRepository>();

    private static IServiceCollection AddDbConnection(this IServiceCollection services)
    {
        services.AddScoped<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(ConnectionString);
            return new Npgsql.NpgsqlConnection(connectionString);
        });
        return services;
    }

    private static IServiceCollection ConfigureFluentMigrator(this IServiceCollection services) =>
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(config => config
                .AddPostgres11_0()
                .WithGlobalConnectionString(provider =>
                    provider.GetRequiredService<IConfiguration>()
                        .GetConnectionString(ConnectionString))
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .Configure<RunnerOptions>(opt =>
                opt.TransactionPerSession = true)
            .AddLogging(lb => lb.AddFluentMigratorConsole());
}
