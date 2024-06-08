using Procoding.ApplicationTracker.Domain.Auth;

namespace Procoding.ApplicationTracker.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task InsertAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

    Task MarkAsUsedAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}
