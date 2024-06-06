
using Microsoft.AspNetCore.Components.Authorization;

namespace Procoding.ApplicationTracker.Web.Auth;

public class TokenProvider : ITokenProvider
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public TokenProvider(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async ValueTask<string?> GetAccessTokenAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value;
    }
}
