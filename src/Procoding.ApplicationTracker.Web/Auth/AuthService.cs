using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Procoding.ApplicationTracker.Web.Auth;

public class ApplicationAuthenticationService : IApplicationAuthenticationService
{
    private readonly IHttpClientFactory? _factory;
    private readonly CircuitServicesAccessor _circuitServicesAccessor;
    private ITokenProvider? _tokenProvider;

    private const string JWT_KEY = nameof(JWT_KEY);
    private const string REFRESH_KEY = nameof(REFRESH_KEY);

    private string? _accessTokenCache;

    public event Action<string?>? LoginChange;

    public ApplicationAuthenticationService(CircuitServicesAccessor circuitServicesAccessor, ITokenProvider tokenProvider, IHttpClientFactory httpClientFactory)
    {
        if (circuitServicesAccessor.Services != null)
        {
            _factory = circuitServicesAccessor.Services.GetRequiredService<IHttpClientFactory>();
            _tokenProvider = circuitServicesAccessor.Services.GetRequiredService<ITokenProvider>();
        }
        else
        {
            _factory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        //_factory = circuitServicesAccessor.Services?.GetRequiredService<IHttpClientFactory>();
        //_tokenProvider = circuitServicesAccessor.Services?.GetRequiredService<ITokenProvider>();

        _circuitServicesAccessor = circuitServicesAccessor;
    }

    public async ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_accessTokenCache) && _tokenProvider is not null)
            _accessTokenCache = await _tokenProvider.GetAccessToken(cancellationToken);

        return _accessTokenCache;
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        var response = await _factory.CreateClient("ServerApi").DeleteAsync("api/authentication/revoke");

        await _tokenProvider.DeleteAccessAndRefreshToken(cancellationToken);

        _accessTokenCache = null;

        await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

        LoginChange?.Invoke(null);
    }

    private static string GetUsername(string token)
    {
        var jwt = new JwtSecurityToken(token);

        return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    }

    public async Task LoginAsync(EmployeeLoginRequestDTO model, CancellationToken cancellationToken = default)
    {

        var response = await _factory.CreateClient("ServerApi").PostAsync("api/employees/login",
                                                    JsonContent.Create(model));

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Login failed.");

        var content = await response.Content.ReadFromJsonAsync<EmployeeLoginResponseDTO>();

        if (content == null)
            throw new InvalidDataException();

        //await _tokenProvider.SaveAccessAndRefreshToken(content.AccessToken, content.RefreshToken, cancellationToken);

        LoginChange?.Invoke(GetUsername(content.AccessToken));

    }

    public async Task<bool> RefreshAsync(CancellationToken cancellationToken = default)
    {


        var model = new TokenRequestDTO
        {
            AccessToken = await _tokenProvider.GetAccessToken(cancellationToken),
            RefreshToken = await _tokenProvider.GetRefreshToken(cancellationToken)
        };

        var response = await _factory.CreateClient("ServerApi").PostAsync("employees/login/refresh",
                                                    JsonContent.Create(model));

        if (!response.IsSuccessStatusCode)
        {
            await LogoutAsync();

            return false;
        }

        var content = await response.Content.ReadFromJsonAsync<EmployeeLoginResponseDTO>();

        if (content == null)
            throw new InvalidDataException();

        //await _tokenProvider.SaveAccessAndRefreshToken(content.AccessToken, content.RefreshToken, cancellationToken);

        _accessTokenCache = content.AccessToken;

        return true;
    }
}