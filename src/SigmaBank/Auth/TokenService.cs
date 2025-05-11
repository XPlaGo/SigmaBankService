using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SigmaBank.Core.Entities;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SigmaBank.Auth;

internal class TokenService(IOptions<TokenSettings> settings) : ITokenService
{
    public string GenerateVerificationToken(string phoneNumber)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, phoneNumber));
        claims.AddClaim(new Claim(ClaimTypes.Name, phoneNumber));
        claims.AddClaim(new Claim(ClaimTypes.MobilePhone, phoneNumber));
        claims.AddClaim(new Claim(ClaimTypes.Role, TokenTypes.Verification.ToString()));

        return GenerateToken(claims);
    }

    public string GenerateConfirmedToken(string phoneNumber)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, phoneNumber));
        claims.AddClaim(new Claim(ClaimTypes.Name, phoneNumber));
        claims.AddClaim(new Claim(ClaimTypes.MobilePhone, phoneNumber));
        claims.AddClaim(new Claim(ClaimTypes.Role, TokenTypes.Confirmed.ToString()));

        return GenerateToken(claims);
    }

    public string GenerateAuthorizedToken(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
        claims.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
        claims.AddClaim(new Claim(ClaimTypes.Role, TokenTypes.Authorized.ToString()));

        return GenerateToken(claims);
    }

    public TokenTypes ResolveTokenType(ClaimsPrincipal claimsPrincipal)
    {
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        culture = (CultureInfo)culture.Clone();
        Thread.CurrentThread.CurrentCulture = culture;

        TokenTypes type = Enum.Parse<TokenTypes>(
            claimsPrincipal
                .FindFirst(ClaimTypes.Role)?
                .Value ?? throw new InvalidOperationException());

        return type;
    }

    public string ResolvePhoneNumber(ClaimsPrincipal claimsPrincipal)
    {
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        culture = (CultureInfo)culture.Clone();
        Thread.CurrentThread.CurrentCulture = culture;

        string phoneNumber = claimsPrincipal
            .FindFirst(ClaimTypes.MobilePhone)?
            .Value ?? throw new InvalidOperationException();

        return phoneNumber;
    }

    public long ResolveUserId(ClaimsPrincipal claimsPrincipal)
    {
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        culture = (CultureInfo)culture.Clone();
        Thread.CurrentThread.CurrentCulture = culture;

        long userId = long.Parse(
            claimsPrincipal
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value ?? throw new InvalidOperationException(),
            culture);

        return userId;
    }

    private string GenerateToken(ClaimsIdentity claims)
    {
        var signingCredentials = new SigningCredentials(
            key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.Key)),
            algorithm: SecurityAlgorithms.HmacSha256);

        var jwtHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwt = jwtHandler.CreateJwtSecurityToken(
            issuer: settings.Value.Issuer,
            audience: settings.Value.Audience,
            subject: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(settings.Value.LifeTimeInSeconds),
            issuedAt: DateTime.UtcNow,
            signingCredentials: signingCredentials);

        string serializedJwt = jwtHandler.WriteToken(jwt);

        return serializedJwt;
    }
}