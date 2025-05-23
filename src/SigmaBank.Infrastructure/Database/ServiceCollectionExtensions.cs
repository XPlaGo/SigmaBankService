﻿using Microsoft.Extensions.DependencyInjection;
using SigmaBank.Core.Repositories.Accounts;
using SigmaBank.Core.Repositories.CardsPrivateData;
using SigmaBank.Core.Repositories.Users;
using SigmaBank.Infrastructure.Database.Accounts;
using SigmaBank.Infrastructure.Database.CardsPrivateData;
using SigmaBank.Infrastructure.Database.Users;

namespace SigmaBank.Infrastructure.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddTransient<IConnectionProvider, ConnectionProvider>();

        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UserRepository>();
        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<ICardsPrivateDataRepository, CardsPrivateDataRepository>();

        return services;
    }
}