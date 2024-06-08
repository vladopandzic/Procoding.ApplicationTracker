using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using System.Net.Http.Json;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

public static class LoginHelper
{
    public const string DEFAULT_CANDIDATE_USER_EMAIL = "email@email.com";

    public const string DEFAULT_CANDIDATE_USER_PASSWORD = "test123";

    public const string DEFAULT_EMPLOYEE_USER_EMAIL = "pandzic.vlado@gmail.com";

    public const string DEFAULT_EMPLOYEE_USER_PASSWORD = "Test123!";

    public static async Task LoginCandidate(HttpClient client)
    {
        var responseLogin = await client.PostAsJsonAsync("candidates/login",
                                                         new CandidateLoginRequestDTO()
                                                        {
                                                            Email = DEFAULT_CANDIDATE_USER_EMAIL,
                                                            Password = DEFAULT_CANDIDATE_USER_PASSWORD
                                                        });
        var responseResult = await responseLogin.Content.ReadFromJsonAsync<CandidateLoginResponseDTO>()!;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseResult!.AccessToken);
    }

    public static async Task LoginEmployee(HttpClient client)
    {
        var responseLogin = await client.PostAsJsonAsync("employees/login",
                                                         new CandidateLoginRequestDTO()
                                                         {
                                                             Email = DEFAULT_EMPLOYEE_USER_EMAIL,
                                                             Password = DEFAULT_EMPLOYEE_USER_PASSWORD
                                                         });
        var responseResult = await responseLogin.Content.ReadFromJsonAsync<CandidateLoginResponseDTO>()!;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseResult!.AccessToken);
    }
}
