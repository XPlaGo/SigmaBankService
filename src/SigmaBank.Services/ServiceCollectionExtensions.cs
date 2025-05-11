using Microsoft.Extensions.DependencyInjection;
using SigmaBank.Core.Services.Users;
using SigmaBank.Services.Users;

namespace SigmaBank.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();

        return services;
    }
}