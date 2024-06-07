namespace Procoding.ApplicationTracker.Domain.Auth;

public interface IIdentityContext
{
    public Guid? UserId { get; }

    public bool? IsEmployee { get; }

    public bool? IsCandidate { get; }

}
