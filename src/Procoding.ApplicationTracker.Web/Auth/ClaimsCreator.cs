using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Procoding.ApplicationTracker.Web.Auth;

public static class ClaimsCreator
{

    public static List<Claim> GetClaimsFromToken(string accessToken, string refreshToken)
    {
        var claims = ParseClaimsFromJwt(accessToken).ToList();

        claims.Add(new Claim("access_token", accessToken));
        claims.Add(new Claim("refresh_token", refreshToken));

        return claims;
    }


    private static IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.Claims;
    }
}
