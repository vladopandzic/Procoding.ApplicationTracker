using Procoding.ApplicationTracker.Web.Auth;
using System.Net;
using System.Net.Http.Headers;

namespace Procoding.ApplicationTracker.Web.Services.Handlers;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly IApplicationAuthenticationService _authenticationService;
    private bool _refreshing;


    public AuthenticationHandler(IApplicationAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await _authenticationService.GetAccessTokenAsync();

        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (!_refreshing && !string.IsNullOrEmpty(accessToken) && response.StatusCode == HttpStatusCode.Unauthorized)
        {
            try
            {
                _refreshing = true;

                if (await _authenticationService.RefreshAsync(cancellationToken))
                {
                    accessToken = await _authenticationService.GetAccessTokenAsync();
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                    response = await base.SendAsync(request, cancellationToken);
                }
            }
            finally
            {
                _refreshing = false;
            }
        }
        return response;
    }
}
