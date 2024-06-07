using Microsoft.AspNetCore.Authorization;

namespace Procoding.ApplicationTracker.Api;

public static class Policies
{
    public const string EmployeeOnly = "EmployeeOnly";

    public static AuthorizationPolicy EmployeeOnlyPolicy()
    {
        return new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .RequireRole("Employee")
                                    .Build();
    }
}