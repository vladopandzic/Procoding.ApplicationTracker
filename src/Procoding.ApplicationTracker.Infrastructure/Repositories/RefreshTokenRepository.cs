using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Auth;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

internal class RefreshTokenRepository : IRefreshTokenRepository
{

    private readonly ApplicationDbContext _dbContext;

    public RefreshTokenRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
    }

    public async Task InsertAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        _ = await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public async Task MarkAsUsedAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        refreshToken.MarkAsUsed();
        _ = _dbContext.RefreshTokens.Update(refreshToken);
        await Task.CompletedTask;
    }
}
