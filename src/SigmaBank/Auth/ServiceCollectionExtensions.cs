using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SigmaBank.Auth;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection accessTokenConfiguration = configuration.GetSection("TokenSettings");
        services.Configure<TokenSettings>(accessTokenConfiguration);

        services.AddTransient<ITokenService, TokenService>();

        IConfigurationSection jwtSettingsConfiguration = configuration.GetSection("TokenSettings");
        services.Configure<TokenService>(jwtSettingsConfiguration);
        TokenSettings tokenSettings = jwtSettingsConfiguration.Get<TokenSettings>()
                                     ?? throw new InvalidOperationException();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireSignedTokens = false,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = false,
                ValidIssuer = tokenSettings.Issuer,
                ValidAudience = tokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
                ClockSkew = TimeSpan.FromMinutes(0),
            });

        services.AddAuthorization();

        return services;
    }
}