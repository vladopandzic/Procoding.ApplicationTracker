using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Procoding.ApplicationTracker.Web.ViewModels.Auth;

namespace Procoding.ApplicationTracker.Web.Pages.Auth;

public partial class Login
{
    [Inject]
    public LoginViewModel ViewModel { get; set; } = default!;


    protected MudForm? mudForm;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected async Task OnSubmit()
    {
        await mudForm!.Validate();

        await ViewModel.LoginAsync();

    }

  

}
