namespace Procoding.ApplicationTracker.Domain.Auth;

public class RefreshToken
{
    private RefreshToken()
    {
    }

    public RefreshToken(DateTimeOffset expiryDate, string accessToken, string refreshToken, Guid employeeId)
    {
        ExpiryDate = expiryDate;
        AccessToken = accessToken;
        Token = refreshToken;
        EmployeeId = employeeId;
    }

    public string Token { get; private set; } = default!;

    public DateTimeOffset ExpiryDate { get; private set; }

    public string AccessToken { get; private set; } = default!;

    public bool Invalidated { get; private set; }

    public bool IsUsed { get; private set; }

    public Guid? EmployeeId { get; private set; }

    public bool HasExpired(TimeProvider timeProvider)
    {
        return ExpiryDate < timeProvider.GetLocalNow();
    }

    public void MarkAsUsed()
    {
        IsUsed = true;
    }

}
