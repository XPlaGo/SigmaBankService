using Microsoft.Extensions.DependencyInjection;
using SigmaBank.Core.Services.Accounts;
using SigmaBank.Core.Services.CardsPrivateData;
using SigmaBank.Core.Services.Users;
using SigmaBank.Services.Acounts;
using SigmaBank.Services.CardsPrivateData;
using SigmaBank.Services.Users;

namespace SigmaBank.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<ICardsPrivateDataService, CardsPrivateDataService>();

        return services;
    }
}