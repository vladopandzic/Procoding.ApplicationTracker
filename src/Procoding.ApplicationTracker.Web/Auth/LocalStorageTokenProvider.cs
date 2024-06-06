
using Blazored.LocalStorage;

namespace Procoding.ApplicationTracker.Web.Auth;

public class LocalStorageTokenProvider : ITokenProvider
{
    private readonly ILocalStorageService _localStorage;

    public LocalStorageTokenProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task DeleteAccessAndRefreshToken(CancellationToken cancellationToken = default)
    {
        await _localStorage.RemoveItemAsync(ITokenProvider.ACCESS_TOKEN, cancellationToken);
        await _localStorage.RemoveItemAsync(ITokenProvider.REFRESH_TOKEN, cancellationToken);

    }

    public async ValueTask<string?> GetAccessToken(CancellationToken cancellationToken = default)
    {
        return await _localStorage.GetItemAsync<string>(ITokenProvider.ACCESS_TOKEN, cancellationToken);
    }

    public async ValueTask<string?> GetRefreshToken(CancellationToken cancellationToken = default)
    {
        return await _localStorage.GetItemAsync<string>(ITokenProvider.REFRESH_TOKEN, cancellationToken);
    }

    public async Task SaveAccessAndRefreshToken(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        await _localStorage.SetItemAsStringAsync(ITokenProvider.ACCESS_TOKEN, accessToken, cancellationToken);
        await _localStorage.SetItemAsStringAsync(ITokenProvider.REFRESH_TOKEN, refreshToken, cancellationToken);
    }
}
