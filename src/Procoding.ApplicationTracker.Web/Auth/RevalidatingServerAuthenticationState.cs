using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Auth;

public class RevalidatingServerAuthenticationState : RevalidatingServerAuthenticationStateProvider
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Constructs an instance of <see cref="RevalidatingServerAuthenticationState"/>.
    /// </summary>
    /// <param name="loggerFactory">A logger factory.</param>
    public RevalidatingServerAuthenticationState(ILoggerFactory loggerFactory, IAuthService authService) : base(loggerFactory)
    {
        _authService = authService;
    }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(8);

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        var currentAccessToken = authenticationState.User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value;
        var currentRefreshToken = authenticationState.User.Claims.FirstOrDefault(x => x.Type == "refresh_token")?.Value;
        var tokenRequest = new TokenRequestDTO() { AccessToken = currentAccessToken!, RefreshToken = currentRefreshToken! };


        var result = await _authService.RefreshLoginToken(tokenRequest, cancellationToken);

        var identity = authenticationState.User.Identities.First();

        var oldClaims = identity.Claims.ToList();

        identity.Claims.ToList().Clear();


        for (int i = 0; i < identity.Claims.Count(); i++)
        {
            var claim = identity.Claims.ToList()[i];
            identity.TryRemoveClaim(claim);
        }

        try
        {
            foreach (var claim in oldClaims)
            {
                identity.TryRemoveClaim(claim);
            }
        }
        catch (Exception ex)
        {
        }
        var newClaims = ClaimsCreator.GetClaimsFromToken(result.Value.AccessToken, result.Value.RefreshToken);

        foreach (var item in newClaims)
        {
            identity.AddClaim(item);
        }

        var newAuthState = Task.FromResult(new AuthenticationState(authenticationState.User));

        SetAuthenticationState(newAuthState);
        return true;


    }
}
