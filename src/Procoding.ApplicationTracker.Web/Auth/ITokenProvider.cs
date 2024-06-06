namespace Procoding.ApplicationTracker.Web.Auth;

public interface ITokenProvider
{

    public const string ACCESS_TOKEN = "ACCESS_TOKEN";


    public const string REFRESH_TOKEN = "REFRESH_TOKEN";

    ValueTask<string?> GetAccessToken(CancellationToken cancellationToken = default);

    ValueTask<string?> GetRefreshToken(CancellationToken cancellationToken = default);

    Task SaveAccessAndRefreshToken(string accessToken, string refreshToken, CancellationToken cancellationToken = default);

    Task DeleteAccessAndRefreshToken(CancellationToken cancellationToken = default);

}
