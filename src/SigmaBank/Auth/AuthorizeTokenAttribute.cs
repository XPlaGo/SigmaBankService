using Microsoft.AspNetCore.Authorization;

namespace SigmaBank.Auth;

internal sealed class AuthorizeTokenAttribute : AuthorizeAttribute
{
    public AuthorizeTokenAttribute(TokenTypes tokenType)
    {
        TokenType = tokenType;
    }

    private TokenTypes _tokenType;

    public TokenTypes TokenType
    {
        get => _tokenType;

        internal set
        {
            _tokenType = value;
            Roles = _tokenType.ToString();
        }
    }
}