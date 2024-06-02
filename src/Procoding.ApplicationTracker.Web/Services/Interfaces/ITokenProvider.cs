namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface ITokenProvider
{
    Task SaveAccessAndRefreshTokens(string accessToken, string refreshToken);

    Task<string?> GetAccessToken();

    Task<string?> GetRefreshToken();

    Task RemoveAccessAndRefreshTokens();
}
