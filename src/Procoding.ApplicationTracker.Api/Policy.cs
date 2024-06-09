using Microsoft.AspNetCore.Authorization;

namespace Procoding.ApplicationTracker.Api;

public static class Policies
{
    public const string EmployeeOnly = "EmployeeOnly";

    public const string CandidateOnly = "CandidateOnly";


    public static AuthorizationPolicy EmployeeOnlyPolicy()
    {
        return new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .RequireRole("Employee")
                                    .Build();
    }

    public static AuthorizationPolicy CandidateOnlyPolicy()
    {
        return new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .RequireRole("Candidate")
                                    .Build();
    }
}