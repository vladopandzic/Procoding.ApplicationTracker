using Procoding.ApplicationTracker.DTOs.Request.Employees;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces
{
    public interface IAuthenticationService
    {
        ValueTask<string> GetAccessTokenAsync();

        Task LoginAsync(EmployeeLoginRequestDTO loginModel, CancellationToken cancellationToken = default);

        Task LogoutAsync(CancellationToken cancellationToken = default);

        Task<bool> RefreshAsync(CancellationToken cancellationToken = default);

        event Action<string?>? LoginChanged;
    }
}
