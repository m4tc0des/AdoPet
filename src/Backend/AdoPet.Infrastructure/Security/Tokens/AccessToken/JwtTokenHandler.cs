using AdoPet.Domain.Entities;
using AdoPet.Domain.Security.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AdoPet.Infrastructure.Security.Tokens.AccessToken;

internal sealed class JwtTokenHandler : IAccessTokenGenerator
{
    private readonly uint _expireTimeMinutes;
    private readonly string _signingKey;

    public JwtTokenHandler(uint expireTimeMinutes, string signingKey)
    {
        _expireTimeMinutes = expireTimeMinutes;
        _signingKey = signingKey;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };
        var tokenDescription = new SecurityTokenDescriptor()
        {
            Expires = DateTime.UtcNow.AddMinutes(_expireTimeMinutes),
            SigningCredentials = new SigningCredentials(Credentials(), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescription);
    }

    private SymmetricSecurityKey Credentials()
    {
        var signingKey = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(signingKey);
    }    
}
