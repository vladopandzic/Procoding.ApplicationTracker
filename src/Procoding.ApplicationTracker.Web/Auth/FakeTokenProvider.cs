
namespace Procoding.ApplicationTracker.Web.Auth;

public class FakeTokenProvider : ITokenProvider
{
    public async Task DeleteAccessAndRefreshToken(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async ValueTask<string?> GetAccessToken(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(string.Empty);
    }

    public async ValueTask<string?> GetRefreshToken(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(string.Empty);
    }

    public async Task SaveAccessAndRefreshToken(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}
