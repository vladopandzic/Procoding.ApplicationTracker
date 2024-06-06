using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Procoding.ApplicationTracker.Web.ViewModels.Companies;

namespace Procoding.ApplicationTracker.Web.Pages.Company;

[Authorize]
public partial class CompanyListPage
{
    [Inject]
    public CompanyListViewModel ViewModel { get; set; } = default!;

    [Inject]
    ILocalStorageService LocalStorage { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await LocalStorage.SetItemAsync("aa", "bb");
        await ViewModel.InitializeViewModel();
        await base.OnInitializedAsync();
    }
}
