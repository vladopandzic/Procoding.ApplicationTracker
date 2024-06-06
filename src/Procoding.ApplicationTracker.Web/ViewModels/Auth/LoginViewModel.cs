using Microsoft.AspNetCore.Components.Authorization;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;

namespace Procoding.ApplicationTracker.Web.ViewModels.Auth;

public class LoginViewModel : EditViewModelBase
{
    private readonly IAuthService _authService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public EmployeeLoginValidator Validator { get; }

    public EmployeeLoginRequestDTO LoginRequest { get; set; } = new EmployeeLoginRequestDTO();

    public LoginViewModel(IAuthService authService,
                          EmployeeLoginValidator validator,
                          AuthenticationStateProvider authenticationStateProvider)
    {
        Validator = validator;
        _authenticationStateProvider = authenticationStateProvider;
        _authService = authService;
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

       
    }
}
