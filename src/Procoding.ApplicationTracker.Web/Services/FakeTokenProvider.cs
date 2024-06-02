using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services
{
    public class FakeTokenProvider : ITokenProvider
    {
        public Task<string?> GetAccessToken()
        {
            return Task.FromResult("");
        }

        public Task<string?> GetRefreshToken()
        {
            return Task.FromResult("");
        }

        public async Task RemoveAccessAndRefreshTokens()
        {
            await Task.CompletedTask;
        }

        public async Task SaveAccessAndRefreshTokens(string accessToken, string refreshToken)
        {
            await Task.CompletedTask;
        }
    }
}
