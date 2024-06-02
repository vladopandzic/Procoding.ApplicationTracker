namespace Procoding.ApplicationTracker.DTOs.Response.Employees;

public class EmployeeLoginResponseDTO
{
    public string AccessToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;

    public string TokenType { get; set; } = "Bearer";

    public int ExpiresIn { get; set; }
}
