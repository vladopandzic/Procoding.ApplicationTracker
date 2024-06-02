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

    public string Token { get; set; } = default!;

    public DateTimeOffset ExpiryDate { get; set; }

    public string AccessToken { get; set; } = default!;

    public bool Invalidated { get; set; }

    public bool IsUsed { get; set; }

    public Guid? EmployeeId { get; set; }

}
