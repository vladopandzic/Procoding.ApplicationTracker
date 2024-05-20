using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.Web.ViewModels.Companies;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.Pages.Company;

public partial class CompanyDetailsPage
{
    [Inject]
    public CompanyDetailsViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public string Id { get; set; }

    protected MudForm? mudForm;

    protected override async Task OnInitializedAsync()
    {
        if (Id == "new")
        {
            await ViewModel.InitializeViewModel(null);
        }
        else
        {
            await ViewModel.InitializeViewModel(Guid.Parse(Id));
        }
        await base.OnInitializedAsync();
    }

    protected async Task OnSubmit()
    {

        var isValid = await ViewModel.IsValidAsync();

        if (isValid)
        {
            await ViewModel.SaveAsync();
        }

    }
}
