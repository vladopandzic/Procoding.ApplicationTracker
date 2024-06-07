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
        var isEmployee = authenticationState.User.IsInRole("Employee");

        var isCandidate = authenticationState.User.IsInRole("Candidate");
        try
        {
            var newAccessToken = "";
            var newRefreshToken = "";

            if (isEmployee)
            {
                var result = await _authService.RefreshLoginTokenForEmployee(tokenRequest, cancellationToken);
                if (!result.IsSuccess)
                {

                    //TODO: give user information about that!
                    return false;
                }
                newAccessToken = result.Value.AccessToken;
                newRefreshToken = result.Value.RefreshToken;
            }
            else if (isCandidate)
            {
                var result = await _authService.RefreshLoginTokenForCandidate(tokenRequest, cancellationToken);
                if (!result.IsSuccess)
                {

                    //TODO: give user information about that!
                    return false;
                }
                newAccessToken = result.Value.AccessToken;
                newRefreshToken = result.Value.RefreshToken;

            }


            var identity = authenticationState.User.Identities.First();

            var oldClaims = identity.Claims.ToList();

            identity.Claims.ToList().Clear();


            for (int i = 0; i < identity.Claims.Count(); i++)
            {
                var claim = identity.Claims.ToList()[i];
                identity.TryRemoveClaim(claim);
            }


            foreach (var claim in oldClaims)
            {
                identity.TryRemoveClaim(claim);
            }


            var newClaims = ClaimsCreator.GetClaimsFromToken(newAccessToken, newRefreshToken);

            foreach (var item in newClaims)
            {
                identity.AddClaim(item);
            }

            var newAuthState = Task.FromResult(new AuthenticationState(authenticationState.User));

            SetAuthenticationState(newAuthState);
        }
        catch (Exception ex)
        {
        }
        return true;


    }
}
