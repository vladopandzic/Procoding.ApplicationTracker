using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Procoding.ApplicationTracker.Web.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenProvider _tokenProvider;

    public CustomAuthStateProvider(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return await AuthenticateUser();
    }

    private async Task<AuthenticationState> AuthenticateUser()
    {
        var token = await _tokenProvider.GetAccessToken();

        if (string.IsNullOrEmpty(token))
        {
            var anonymous = new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(anonymous));
        }

        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, authenticationType: "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.Claims;
    }

    public async Task AuthenticateUser(string userIdentifier)
    {
        var state = await AuthenticateUser();

        NotifyAuthenticationStateChanged(
            Task.FromResult(state));
    }

}
