namespace Procoding.ApplicationTracker.Web.Auth;

public interface ITokenProvider
{
    ValueTask<string?> GetAccessTokenAsync();
}
