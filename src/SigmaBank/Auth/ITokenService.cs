using SigmaBank.Core.Entities;
using System.Security.Claims;

namespace SigmaBank.Auth;

internal interface ITokenService
{
    public string GenerateVerificationToken(string phoneNumber);

    public string GenerateConfirmedToken(string phoneNumber);

    public string GenerateAuthorizedToken(User user);

    public TokenTypes ResolveTokenType(ClaimsPrincipal claimsPrincipal);

    public string ResolvePhoneNumber(ClaimsPrincipal claimsPrincipal);

    public long ResolveUserId(ClaimsPrincipal claimsPrincipal);
}