using Procoding.ApplicationTracker.DTOs.Request.Employees;

namespace Procoding.ApplicationTracker.Web.Auth
{
    public interface IApplicationAuthenticationService
    {
        ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);

        Task LoginAsync(EmployeeLoginRequestDTO loginModel, CancellationToken cancellationToken = default);

        Task LogoutAsync(CancellationToken cancellationToken = default);

        Task<bool> RefreshAsync(CancellationToken cancellationToken = default);

        event Action<string?>? LoginChange;
    }
}
