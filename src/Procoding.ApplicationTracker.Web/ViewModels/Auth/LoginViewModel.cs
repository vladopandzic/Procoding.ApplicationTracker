using Microsoft.AspNetCore.Components.Authorization;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;

namespace Procoding.ApplicationTracker.Web.ViewModels.Auth;

public class LoginViewModel : EditViewModelBase
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public EmployeeLoginValidator Validator { get; }

    public EmployeeLoginRequestDTO LoginRequest { get; set; } = new EmployeeLoginRequestDTO();

    public LoginViewModel(IAuthService authService,
                          ITokenProvider tokenProvider,
                          EmployeeLoginValidator validator,
                          AuthenticationStateProvider authenticationStateProvider)
    {
        Validator = validator;
        _authenticationStateProvider = authenticationStateProvider;
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    public async Task LoginAsync(CancellationToken cancellationToken = default)
    {
        var validationResult = await Validator.ValidateAsync(LoginRequest);
        if (!validationResult.IsValid)
        {
            return;
        }
        IsLoading = true;
        var response = await _authService.LoginEmployee(LoginRequest, cancellationToken);
        IsLoading = false;

        if (response.IsSuccess)
        {
            //await _tokenProvider.SaveAccessAndRefreshToken(response.Value.AccessToken, response.Value.RefreshToken, cancellationToken);
            await UpdateState();
        }
    }

    public Task UpdateState()
    {
        Task<AuthenticationState> authStateTask = _authenticationStateProvider.GetAuthenticationStateAsync();

        if (_authenticationStateProvider is CustomAuthStateProvider customAuthStateProvider)
        {
            customAuthStateProvider.AuthenticateUser("dss");
        }
        
        return Task.CompletedTask;
    }
}
