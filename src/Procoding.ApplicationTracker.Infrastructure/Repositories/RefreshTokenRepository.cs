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

    public async Task<RefreshToken?> GetByToken(string token)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task Insert(RefreshToken refreshToken)
    {
        _ = await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task MarkAsUsed(RefreshToken refreshToken)
    {
        _ = _dbContext.RefreshTokens.Update(refreshToken);
        await Task.CompletedTask;
    }
}
