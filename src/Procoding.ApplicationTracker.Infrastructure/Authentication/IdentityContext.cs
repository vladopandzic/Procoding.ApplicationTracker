using Microsoft.AspNetCore.Http;
using Procoding.ApplicationTracker.Domain.Auth;
using System.Security.Claims;

namespace Procoding.ApplicationTracker.Infrastructure.Authentication;

internal class IdentityContext : IIdentityContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public Guid? UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return new Guid(userIdClaim.Value);
            }
            else
            {
                return null;
            }
        }
    }

    public bool? IsCandidate
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole("Candidate");
        }
    }

    public bool? IsEmployee
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole("Employee");
        }
    }
}
