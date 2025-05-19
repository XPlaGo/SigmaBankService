using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SigmaBank.Infrastructure.Database;

namespace SigmaBank.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddMigrations();
    }

    private static IServiceCollection AddMigrations(this IServiceCollection services)
    {
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(static rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(static provider =>
                {
                    IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
                    string? connectionString = configuration.GetConnectionString("PostgresConnection");
                    return connectionString;
                })
                .ScanIn(typeof(ServiceCollectionExtensions).Assembly).For.Migrations())
            .AddLogging(static lb => lb.AddFluentMigratorConsole());

        return services;
    }
}