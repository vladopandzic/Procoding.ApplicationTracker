using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using System.Security.Claims;

namespace Procoding.ApplicationTracker.Web.Controllers;

public class LoginController : Controller
{
    readonly IAuthService _authService;
    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(EmployeeLoginRequestDTO model, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginEmployee(model, cancellationToken);
            if (result.IsSuccess)
            {
                var claims = ClaimsCreator.GetClaimsFromToken(result.Value.AccessToken, result.Value.RefreshToken);

                var identity = new ClaimsIdentity(claims, authenticationType: "jwt");
                var user = new ClaimsPrincipal(identity);


                var authenticationProperties = new AuthenticationProperties();
                authenticationProperties.StoreTokens([new AuthenticationToken() { Name = "access_token", Value = result.Value.AccessToken },
                                                      new AuthenticationToken() { Name = "refresh_token", Value = result.Value.RefreshToken }]);


                await HttpContext.SignInAsync(user, authenticationProperties);

                return Redirect("/");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(model);
    }
}
