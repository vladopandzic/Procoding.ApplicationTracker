using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Infrastructure.Authentication;

public class CandidateUserStore : UserStore<Candidate, IdentityRole<Guid>, ApplicationDbContext, Guid>, IDisposable
{
    /// <summary>
    /// Constructs a new instance of <see cref="CandidateUserStore`4"/>.
    /// </summary>
    /// <param name="context">The <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.</param>
    /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
    public CandidateUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer) : base(context, describer)
    {
        AutoSaveChanges = false;
    }

    public override Task<string?> GetEmailAsync(Candidate user, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        return Task.FromResult((string?)user.Email.Value);
    }
}
