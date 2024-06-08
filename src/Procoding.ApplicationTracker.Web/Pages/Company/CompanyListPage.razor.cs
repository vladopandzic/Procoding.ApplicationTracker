using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Procoding.ApplicationTracker.Web.ViewModels.Companies;

namespace Procoding.ApplicationTracker.Web.Pages.Company;

[Authorize(Roles = "Employee")]
public partial class CompanyListPage
{
    [Inject]
    public CompanyListViewModel ViewModel { get; set; } = default!;


    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeViewModel();
        await base.OnInitializedAsync();
    }
}
