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
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(provider =>
                {
                    IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
                    string? connectionString = configuration.GetConnectionString("PostgresConnection");
                    return connectionString;
                })
                .ScanIn(typeof(ServiceCollectionExtensions).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        return services;
    }
}