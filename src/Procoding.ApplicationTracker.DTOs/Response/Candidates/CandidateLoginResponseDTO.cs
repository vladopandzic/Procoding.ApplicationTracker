namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateLoginResponseDTO
{
    public string AccessToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;

    public string TokenType { get; set; } = "Bearer";

    public int ExpiresIn { get; set; }
}
