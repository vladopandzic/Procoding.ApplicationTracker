using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Procoding.ApplicationTracker.Application.Authentication.JwtTokens;


public class JwtTokenCreator<T> : IJwtTokenCreator<T>
{
    private const int DEFAULT_EXPIRATION_TIME_IN_SECONDS = 5000;
    private readonly JwtTokenOptions<T> _options;
    private List<Claim> _claims;

    public JwtTokenCreator(JwtTokenOptions<T> options)
    {
        _claims = new List<Claim>();
        _options = options;
    }

    public string CreateJwtToken(List<Claim> claims)
    {
        JwtSecurityToken token = CreateJwtToken(claims: claims, options: _options, notBefore: DateTime.Now);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public SigningCredentials GetDefaultSigningCredentials(string secretkey)
    {
        var secretBytes = Encoding.UTF8.GetBytes(secretkey);
        var key = new SymmetricSecurityKey(secretBytes);
        var algorithm = SecurityAlgorithms.HmacSha256;
        var signingCredentials = new SigningCredentials(key, algorithm);
        return signingCredentials;
    }

    private JwtSecurityToken CreateJwtToken<T>(List<Claim> claims, JwtTokenOptions<T> options, DateTime notBefore)
    {
        var signingCredentials = GetDefaultSigningCredentials(options.SecretKey);
        var token = new JwtSecurityToken(issuer: options.Issuer,
                                         audience: options.Audience,
                                         claims: claims,
                                         notBefore: notBefore,
                                         expires: notBefore.AddSeconds(options.ExpiresInSeconds),
                                         signingCredentials: signingCredentials);
        return token;
    }
}

