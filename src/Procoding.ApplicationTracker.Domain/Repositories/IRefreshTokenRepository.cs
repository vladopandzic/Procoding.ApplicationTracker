using Procoding.ApplicationTracker.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task Insert(RefreshToken refreshToken);

    Task<RefreshToken?> GetByToken(string token);

    Task MarkAsUsed(RefreshToken refreshToken);
}
