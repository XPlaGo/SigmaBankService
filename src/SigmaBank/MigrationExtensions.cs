using FluentMigrator.Runner;

namespace SigmaBank;

internal static class MigrationExtensions
{
    public static IApplicationBuilder Migrate(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();

        return app;
    }
}