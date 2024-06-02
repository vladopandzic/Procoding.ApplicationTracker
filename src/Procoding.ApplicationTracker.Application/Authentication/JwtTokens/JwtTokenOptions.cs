namespace Procoding.ApplicationTracker.Application.Authentication.JwtTokens;

public class JwtTokenOptions<T>
{
    public string Audience { get; set; } = default!;

    public string Issuer { get; set; } = default!;

    public int ExpiresInSeconds { get; set; } = default!;

    public string SecretKey { get; set; } = default!;
}
